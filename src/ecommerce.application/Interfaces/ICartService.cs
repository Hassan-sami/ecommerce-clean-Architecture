using System.Linq.Expressions;
using ecommerce.Domain.Enitities;
using ecommerce.Domain.Interfaces;

namespace ecommerce.Application.Interfaces;

public interface ICartService
{
    ValueTask<Cart> CreateCart(Cart cart);
    ValueTask<Cart?> GetCartById(Guid id);
    
    ValueTask DeleteCart(Guid Id);
    ValueTask<Cart?> GetCartByUserId(Guid UserId);
    
    ValueTask<Cart?> GetCartWithSpecAsync(ISpecification<Cart> spec);
    
    ValueTask UpdateAsync(Cart cart);
}