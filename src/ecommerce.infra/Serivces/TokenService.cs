using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ecommerce.Application.Interfaces;
using ecommerce.Application.options;
using ecommerce.Domain.Enitities.Identities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ecommerce.infra.Serivces;

public class TokenService : ITokenSerivce
{
    private readonly UserManager<AppUser> _userManager;
    private readonly  JwtOptions _options;

    public TokenService(IOptionsSnapshot<JwtOptions> options,UserManager<AppUser> userManager)
    {
        _userManager = userManager;
        _options = options.Value;
    }
    public async Task<string> Generate(ClaimsPrincipal user, JwtOptions options)
    {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var id = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id is not null)
            {
                var dbUser = await _userManager.FindByIdAsync(id);
                if (dbUser is not null)
                {
                    var roles = await _userManager.GetRolesAsync(dbUser);
                    user.Claims.Append(new Claim(ClaimTypes.Role, roles.FirstOrDefault(r => r.Contains("Admin")) ?? "user"));
                }
            }
            
            var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(_options.Key), out _);
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Audience = options.Audience,
                Issuer = options.Issuer,
                Expires = DateTime.Now.AddMinutes(Convert.ToInt32(options.LifeTime)),
                IssuedAt = DateTime.Now,
                Subject = user.Identity as ClaimsIdentity,
                SigningCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256Signature),
            };
            var token = handler.CreateToken(securityTokenDescriptor);
            var tokenstring = handler.WriteToken(token);
            return tokenstring;
    }
}