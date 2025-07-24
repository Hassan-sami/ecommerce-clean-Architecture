using ecommerce.Domain.Enitities.Identities;
using ecommerce.Domain.Interfaces;
using ecommerce.infra.Context;

namespace ecommerce.infra.Repos;

public class RefreshTokenRepository :Repository<RefreshToken> , IRefreshTokenRepo 
{
    public RefreshTokenRepository(AppDbContext dbContext) : base(dbContext)
    {
        
    }
}