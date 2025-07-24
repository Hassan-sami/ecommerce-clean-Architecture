using ecommerce.Domain.Enitities;

namespace ecommerce.Application.Interfaces;

public interface ICartService
{
    public ValueTask<Cart> CreateCart(Cart cart);
    public ValueTask<Cart?> GetCartById(Guid id);
    
    public ValueTask DeleteCart(Guid Id);
    
}