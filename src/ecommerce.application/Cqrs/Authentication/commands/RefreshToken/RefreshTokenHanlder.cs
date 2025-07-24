using System.Security.Claims;
using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Authentication.Responses;
using ecommerce.Application.Interfaces;
using ecommerce.Application.options;
using ecommerce.Application.Resources;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace ecommerce.Application.Cqrs.Authentication.commands.RefreshToken;

public class RefreshTokenHanlder(
    IStringLocalizer<Resource> stringLocalizer,
    IRefreshTokenService refreshTokenService,
    ITokenSerivce tokenSerivce,
    IOptionsSnapshot<JwtOptions> optionsSnapshot) : ResponseHandler(stringLocalizer),
    IRequestHandler<RefreshTokenCommand, Response<JwtResponse>>
{
    public async Task<Response<JwtResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var isValid = await refreshTokenService.IsvalidToken(request.refreshToken);
        if (!isValid.Item1)
            return BadRequest<JwtResponse>();
        if (isValid.Item2 is null)
            return BadRequest<JwtResponse>();
        if (isValid.Item2.AppUser is null)
            return BadRequest<JwtResponse>();
        var claims = new HashSet<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, isValid.Item2.AppUser.Id),
            new Claim(ClaimTypes.Email, isValid.Item2.AppUser.Email),
            new Claim(ClaimTypes.Name, isValid.Item2.AppUser.UserName),
            new Claim(ClaimTypes.GivenName, isValid.Item2.AppUser.FirstName),
            new Claim(ClaimTypes.Surname, isValid.Item2.AppUser.UserName),
            // new Claim(ClaimTypes.Role, u)
        };
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims));
        var options = optionsSnapshot.Value;
        var accessToken = await tokenSerivce.Generate(user, options);
        RefreshTokenResponse refreshTokenResponse = default;
        if (isValid.Item2.ExpiryDate <= DateTime.Now.AddHours(2))
        {
            refreshTokenResponse = refreshTokenService.RefreshToken();
            await refreshTokenService.RevokeToken(isValid.Item2.Id);
        }

        var result = new JwtResponse()
        {
            Token = accessToken,
            RefreshToken = refreshTokenResponse is not null
                ? refreshTokenResponse
                : new RefreshTokenResponse()
                {
                    Token   = isValid.Item2.Token,
                    Expires = isValid.Item2.ExpiryDate,
                    CreatedAt = isValid.Item2.AddedTime,
                }
        };
        return Success<JwtResponse>(result);
    }
}