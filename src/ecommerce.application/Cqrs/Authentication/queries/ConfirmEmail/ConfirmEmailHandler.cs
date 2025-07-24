using ecommerce.Application.Base;
using ecommerce.Application.Interfaces;
using ecommerce.Application.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace ecommerce.Application.Cqrs.Authentication.queries.ConfirmEmail;

public class ConfirmEmailHandler(IStringLocalizer<Resource> stringLocalizer, IAuthenticationSerive authenticationSerive) : ResponseHandler(stringLocalizer), IRequestHandler<ConfirmEmailQuery ,Response<string>>
{
    public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
    { 
            var confirmEmail = await authenticationSerive.ConfirmEmail(request.UserId, request.code);
            if (confirmEmail=="ErrorWhenConfirmEmail")
                return BadRequest<string>();
            return Success<string>("Email verification successful");
    }
}