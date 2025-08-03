using System.Linq.Expressions;
using ecommerce.Application.Interfaces;
using ecommerce.Domain.Enitities;
using ecommerce.Domain.Interfaces;

namespace ecommerce.Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;

    public CartService(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }
    public async ValueTask<Cart> CreateCart(Cart cart)
    {
        return await _cartRepository.AddCartItem(cart);
    }

    public async ValueTask<Cart> GetCartById(Guid id)
    {
        return (await _cartRepository.GetAsync(c => c.Id == id)).FirstOrDefault();
    }

    public async ValueTask DeleteCart(Guid Id)
    {
        var cart = (await _cartRepository.GetAsync(c => c.Id == Id)).FirstOrDefault();
        if (cart == null)
            return;
        await _cartRepository.DeleteAsync(cart);
        
    }

    public async ValueTask<Cart?> GetCartByUserId(Guid UserId)
    {
        return  (await _cartRepository.GetAsync(c => c.UserId == UserId)).FirstOrDefault();
    }

    public async ValueTask<Cart?> GetCartWithSpecAsync(ISpecification<Cart> spec)
    {
        return (await _cartRepository.GetAsync(spec)).FirstOrDefault();
    }

    public async ValueTask UpdateAsync(Cart cart)
    {
        await _cartRepository.UpdateAsync(cart);
    }
}