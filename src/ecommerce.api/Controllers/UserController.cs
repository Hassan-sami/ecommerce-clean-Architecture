
using System.Security.Claims;
using ecommerce.api.Common;
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
public class UserController : BaseController
{
    private readonly IOptionsSnapshot<JwtOptions> _optionsSnapshot;
    private readonly ITokenSerivce _tokenSerivce;
    private readonly JwtOptions _options;

    public UserController(IMediator mediator,
                    IOptionsSnapshot<JwtOptions> optionsSnapshot,
                            ITokenSerivce tokenSerivce) : base(mediator)
    {
        _optionsSnapshot = optionsSnapshot;
        _tokenSerivce = tokenSerivce;
        _options = _optionsSnapshot.Value;
    }
        
    
}