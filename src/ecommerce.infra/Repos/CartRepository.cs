using ecommerce.Domain.Enitities;
using ecommerce.Domain.Interfaces;
using ecommerce.infra.Context;

namespace ecommerce.infra.Repos;

public class CartRepository :  Repository<Cart>, ICartRepository
{
    public CartRepository(AppDbContext dbContext) : base(dbContext)
    {
    }


    public async ValueTask<Cart> AddCartItem(Cart cart)
    {
        return await base.AddAsync(cart);
    }
}