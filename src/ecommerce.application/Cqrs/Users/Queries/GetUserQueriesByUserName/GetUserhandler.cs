using ecommerce.Application.Base;
using ecommerce.Application.Resources;
using ecommerce.Domain.Enitities.Identities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace ecommerce.Application.Cqrs.Users.Queries.GetUserQueriesByUserName;

public class GetUserhandler : ResponseHandler,IRequestHandler<GetUserByUserNameQuery, Response<AppUser>> ,
                                IRequestHandler<GetUserByEmailQuery, Response<AppUser>>
{
    private readonly IStringLocalizer<Resource> _localizer;
    private readonly UserManager<AppUser> _userManager;

    public GetUserhandler(IStringLocalizer<Resource> localizer,UserManager<AppUser> userManager) : base(localizer)
    {
        _localizer = localizer;
        _userManager = userManager;
    }
    public async Task<Response<AppUser>> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null)
            return NotFound<AppUser>(_localizer[LocalizationConstants.UserNotFound]);
        return Success<AppUser>(user);
    }

    public async Task<Response<AppUser>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return NotFound<AppUser>(_localizer[LocalizationConstants.UserNotFound]);
        return Success<AppUser>(user);
    }
}