using System.Security.Claims;
using ecommerce.api.Common;
using ecommerce.Application.Cqrs.Authentication.commands.CreateUser;
using ecommerce.Application.Cqrs.Authentication.commands.RefreshToken;
using ecommerce.Application.Cqrs.Authentication.queries.ConfirmEmail;
using ecommerce.Application.Cqrs.Authentication.queries.SignInQueries;
using ecommerce.Application.Cqrs.Users.Commands.CreateUser;
using ecommerce.Application.Cqrs.Users.Queries.GetUserQueriesByUserName;
using ecommerce.Application.Cqrs.Users.Queries.SignInQueries;
using ecommerce.Application.Interfaces;
using ecommerce.Application.options;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ecommerce.api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : BaseController
{
    private readonly ITokenSerivce _tokenSerivce;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly JwtOptions _options;

    public AuthenticationController(IMediator mediator,
        IOptionsSnapshot<JwtOptions> optionsSnapshot,
        ITokenSerivce tokenSerivce,
        IRefreshTokenService refreshTokenService) : base(mediator)
    {
        _tokenSerivce = tokenSerivce;
        _refreshTokenService = refreshTokenService;
        _options = optionsSnapshot.Value;
    }

    [HttpGet("[action]")]
    public IActionResult SignInWithGoogle()
    {
        var url = Url.Action(nameof(Token), "User", Request.Scheme);
        var properties = new AuthenticationProperties() { RedirectUri = url };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet("[action]")]
    [Authorize(AuthenticationSchemes = GoogleDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Token()
    {
        var query = new GetUserByEmailQuery(User.FindFirstValue(ClaimTypes.Email));
        var res = await mediator.Send(query);
        if (res.Data is null)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email is null)
                return BadRequest();
            var createUserCommand = new CreateUserCommand(email,
                User.FindFirstValue(ClaimTypes.GivenName)?? "",
                User.FindFirstValue(ClaimTypes.Surname)??"");
           var appUserCreateionResponse =  await mediator.Send(createUserCommand);
           if(!appUserCreateionResponse.Succeeded)
               return BadRequest();
        }
        var claims = new HashSet<Claim>(User.Claims.Where( c => c.Type != ClaimTypes.NameIdentifier ));
        claims.Add(new Claim(ClaimTypes.NameIdentifier, res.Data.Id));
        var idenity = new ClaimsIdentity(claims);
        ClaimsPrincipal u = new ClaimsPrincipal(idenity);
        var signInQuery = new SignInQueryWithClaimPrinciple(User,res.Data.Id);
        var response = await  mediator.Send(signInQuery);
        HttpContext.Response.Cookies.Delete(".AspNetCore.cookieg");
        return NewResult(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> SignIn([FromBody]SignInQuery query)
    {
        var response = await mediator.Send(query);
        return NewResult(response);
    }
    [HttpPost("[action]")]
    
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand command)
    {
        var result = await mediator.Send(command);
        return NewResult(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register([FromBody]RegisterCommand command)
    {
        var result =await mediator.Send(command);
        return NewResult(result);
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery query)
    {
         var result= await mediator.Send(query);
        return NewResult(result);
    }
}