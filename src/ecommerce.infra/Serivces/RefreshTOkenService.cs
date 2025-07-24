using System.Security.Cryptography;
using System.Text;
using ecommerce.Application.Cqrs.Authentication.Responses;
using ecommerce.Application.Interfaces;
using ecommerce.Domain.Enitities.Identities;
using ecommerce.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ecommerce.infra.Serivces;

public class RefreshTOkenService : IRefreshTokenService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IRefreshTokenRepo _refreshTokenRepo;

    public RefreshTOkenService(UserManager<AppUser> userManager,IRefreshTokenRepo refreshTokenRepo)
    {
        _userManager = userManager;
        _refreshTokenRepo = refreshTokenRepo;
    }
    public RefreshTokenResponse RefreshToken()
    {
        var randarrey = new byte[32];
        var gen = RandomNumberGenerator.Create();
        gen.GetBytes(randarrey);
        var response = new RefreshTokenResponse()
        {
            Expires = DateTime.UtcNow.AddDays(14),
            CreatedAt = DateTime.UtcNow,
            Token = Encoding.UTF8.GetString(randarrey),
        };
        return response;
    }

    public async Task<bool> SaveAsync(string userId, RefreshToken refreshToken)
    {
        var user = refreshToken.AppUser;
        if (user == null)
            user = await _userManager.FindByIdAsync(userId);
        if(user is null)
            return false;
        refreshToken.AppUser = user;
        var result= await _refreshTokenRepo.AddAsync(refreshToken);
        return true;
    }

    public async Task<(bool, RefreshToken)> IsvalidToken(string token)
    {
        var refreshTokens = await _refreshTokenRepo.GetAsync(t => t.Token == token && t.IsRevoked == false);
        var refreshToken = refreshTokens.FirstOrDefault();
        if(refreshToken == null)
            return (false,null);
        if(refreshToken.ExpiryDate < DateTime.UtcNow)
            return (false,null);
        return (true,refreshToken);
    }

    public async Task RevokeToken(Guid id)
    {
        var token = await _refreshTokenRepo.GetByIdAsync(id);
        token.IsRevoked = true;
        await _refreshTokenRepo.UpdateAsync(token);
    }

    
}