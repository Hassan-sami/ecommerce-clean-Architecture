using ecommerce.Domain.Enitities;

namespace ecommerce.Domain.Interfaces;

public interface ICartRepository : IRepository<Cart>
{
    public ValueTask<Cart> AddCartItem(Cart cart);
    
    
}