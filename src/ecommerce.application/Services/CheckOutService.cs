using ecommerce.Application.Cqrs.Carts.Reponses;
using ecommerce.Application.Interfaces;
using ecommerce.Domain.Enitities;
using ecommerce.Domain.Interfaces;

namespace ecommerce.Application.Services;

public class CheckOutService : ICheckOutService
{
    private readonly IRepository<Order> _orderRepository;
    private readonly ICartRepository _cartRepository;

    public CheckOutService(IRepository<Order> orderRepository, ICartRepository cartRepository)
    {
        _orderRepository = orderRepository;
        _cartRepository = cartRepository;
    }
    public async ValueTask<Order> CheckOut(Cart cart, PersonalInfo pernsonalInfo)
    {
        var order = Order.Create(cart);
        await _cartRepository.DeleteAsync(cart);
        // to be added payment process
       return await _orderRepository.AddAsync(order);
       
    }
}