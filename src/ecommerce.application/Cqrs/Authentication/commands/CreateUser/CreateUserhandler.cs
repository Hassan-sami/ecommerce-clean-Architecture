using AutoMapper;
using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Authentication.Responses;
using ecommerce.Application.Resources;
using ecommerce.Domain.Enitities.Identities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace ecommerce.Application.Cqrs.Authentication.commands.CreateUser;

public class CreateUserhandler :ResponseHandler , IRequestHandler<CreateUserCommand, Response<AppUserCreated>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IStringLocalizer<Resource> _localizer;
    private readonly IMapper _mapper;

    public CreateUserhandler(UserManager<AppUser> userManager, IStringLocalizer<Resource> localizer, IMapper mapper) : base(localizer)
    {
        _userManager = userManager;
        _localizer = localizer;
        _mapper = mapper;
    }
    public async Task<Response<AppUserCreated>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var AppUser = new AppUser()
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.firstName,
            LastName = request.lastName,
        };
         var user = await _userManager.FindByEmailAsync(request.Email);
         if (user is not null)
             return BadRequest<AppUserCreated>();
         AppUser.EmailConfirmed = true;
        var result = await _userManager.CreateAsync(AppUser);
        if (result.Succeeded)
        {
            var appUserCreated = _mapper.Map<AppUserCreated>(AppUser);
            return Created(appUserCreated);
        }
        return BadRequest<AppUserCreated>(_localizer[LocalizationConstants.BadRequest]);

    }
}