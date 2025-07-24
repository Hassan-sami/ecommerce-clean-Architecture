using System.Security.Claims;
using System.Security.Cryptography;
using ecommerce.Application.options;

namespace ecommerce.Application.Interfaces;

public interface ITokenSerivce
{
    Task<string> Generate(ClaimsPrincipal user,  JwtOptions options);
}