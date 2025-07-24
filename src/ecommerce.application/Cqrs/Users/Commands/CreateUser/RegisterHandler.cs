using AutoMapper;
using ecommerce.Application.Base;
using ecommerce.Application.Interfaces;
using ecommerce.Application.Resources;
using ecommerce.Domain.Enitities.Identities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc;
namespace ecommerce.Application.Cqrs.Users.Commands.CreateUser;

public class RegisterHandler(IStringLocalizer<Resource> stringLocalizer,
                                UserManager<AppUser> userManager, IMapper mapper, 
                                IUserSerivce userSerivce) : ResponseHandler(stringLocalizer), IRequestHandler<RegisterCommand, Response<string>>
{
    public async Task<Response<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        
        if(request.Password != request.ConfirmPassword)
            return BadRequest<string>(stringLocalizer[LocalizationConstants.BadRequest]);
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user != null)
            return BadRequest<string>();
        var appUser = mapper.Map<AppUser>(request);
        appUser.UserName = request.Email;
        var created = await userSerivce.CreateUser(appUser, request.Password);
        if (created)
            return Success("Created user");
        return BadRequest<string>(stringLocalizer[LocalizationConstants.BadRequest]);
    }
}