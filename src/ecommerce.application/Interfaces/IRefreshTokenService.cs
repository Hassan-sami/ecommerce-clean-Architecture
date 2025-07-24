using ecommerce.Application.Cqrs.Authentication.Responses;
using ecommerce.Domain.Enitities.Identities;

namespace ecommerce.Application.Interfaces;

public interface IRefreshTokenService
{
    RefreshTokenResponse RefreshToken();
    Task<bool> SaveAsync(string userId, RefreshToken refreshToken);
    public Task<(bool, RefreshToken)> IsvalidToken(string token);
    public Task RevokeToken(Guid id);
}