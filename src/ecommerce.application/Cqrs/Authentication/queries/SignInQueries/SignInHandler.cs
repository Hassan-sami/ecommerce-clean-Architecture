using System.Security.Claims;
using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Authentication.queries.SignInQueries;
using ecommerce.Application.Cqrs.Authentication.Responses;
using ecommerce.Application.Cqrs.Users.Queries.SignInQueries;
using ecommerce.Application.Interfaces;
using ecommerce.Application.options;
using ecommerce.Application.Resources;
using ecommerce.Domain.Enitities.Identities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;

namespace ecommerce.Application.Cqrs.Authentication.Queries.SignInQueries;

public class SignInHandler : ResponseHandler,IRequestHandler<SignInQuery, Response<SignInResponse>>,
    IRequestHandler<SignInQueryWithClaimPrinciple, Response<SignInResponse>>
                            
{
    private readonly IStringLocalizer<Resource> _localizer;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenSerivce _tokenSerivce;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly JwtOptions _options;

    public SignInHandler(IStringLocalizer<Resource> localizer, 
                        UserManager<AppUser> userManager, 
                        SignInManager<AppUser> signInManager,
                        ITokenSerivce tokenSerivce,
                         IOptionsSnapshot<JwtOptions> optionsSnapshot,
                         IRefreshTokenService refreshTokenService) : base(localizer)
    {
        _localizer = localizer;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenSerivce = tokenSerivce;
        _refreshTokenService = refreshTokenService;
        _options = optionsSnapshot.Value;
    }
    public async Task<Response<SignInResponse>> Handle(SignInQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return NotFound<SignInResponse>(_localizer[LocalizationConstants.NotFound]);
        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if(!result.Succeeded)
            return NotFound<SignInResponse>(_localizer[LocalizationConstants.PasswordOrUserIsIncorrect]);
        if (!user.EmailConfirmed)
            return BadRequest<SignInResponse>(_localizer[LocalizationConstants.EmailNotConfirmed]);
        var claims = new HashSet<Claim>();
        claims.Add(new Claim(ClaimTypes.Email, user?.Email??""));
        claims.Add(new Claim(ClaimTypes.GivenName, user?.FirstName?? ""));
        claims.Add(new Claim(ClaimTypes.Surname, user?.LastName?? ""));
        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
        claims.Add(new Claim(ClaimTypes.Name, user?.UserName?? ""));
        var identity = new ClaimsIdentity(claims);
        var principal = new ClaimsPrincipal(identity);
        var response = new SignInResponse()
        {
            FirstName = user?.FirstName??"",
            LastName = user?.LastName??"",
            Email = user?.Email??"",
            response = new JwtResponse()
            {
                Token = await _tokenSerivce.Generate(principal,_options),
                RefreshToken = _refreshTokenService.RefreshToken()
            }
        };
        // to do save the refresh token to the database
        var token = new RefreshToken()
        {
            Token = response.response.RefreshToken.Token,
            AppUserId = user.Id,
            IsUsed = false,
            IsRevoked = false,
            RefreshTokenString = response.response.RefreshToken.Token,
            JwtId = response.response.Token,
            AddedTime = response.response.RefreshToken.CreatedAt,
            ExpiryDate = response.response.RefreshToken.Expires,
            AppUser = user
        };
        var saveResult = await _refreshTokenService.SaveAsync(user.Id,token);
        if (saveResult == false)
            return BadRequest<SignInResponse>();
        return Success(response);
    }

    public async Task<Response<SignInResponse>> Handle(SignInQueryWithClaimPrinciple request, CancellationToken cancellationToken)
    {
        var response = new SignInResponse()
        {
            FirstName = request.User.FindFirstValue(ClaimTypes.Name),
            LastName = request.User.FindFirstValue(ClaimTypes.Surname),
            Email = request.User.FindFirstValue(ClaimTypes.Email),
            response = new JwtResponse()
            {
                Token = await _tokenSerivce.Generate(request.User,_options),
                RefreshToken = _refreshTokenService.RefreshToken()
            }
        };
        var token = new RefreshToken()
        {
            Token = response.response.RefreshToken.Token,
            AppUserId = request.UserId,
            IsUsed = false,
            IsRevoked = false,
            RefreshTokenString = response.response.RefreshToken.Token,
            JwtId = response.response.Token,
            AddedTime = response.response.RefreshToken.CreatedAt,
            ExpiryDate = response.response.RefreshToken.Expires
        };
        var result = await _refreshTokenService.SaveAsync(request.UserId,token);
        if (result == false)
            return BadRequest<SignInResponse>();
        return Success(response);
    }
}