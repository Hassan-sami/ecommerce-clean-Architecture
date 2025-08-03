using ecommerce.Application.Interfaces;
using ecommerce.Domain.Enitities;
using ecommerce.Domain.Interfaces;

namespace ecommerce.Application.Services;

public class OrderSerivce : IOrderService
{
    private readonly IRepository<Order> _orderRepository;

    public OrderSerivce(IRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<IReadOnlyList<Order>> getOrders(ISpecification<Order> specification)
    {
        return await _orderRepository.GetAsync(specification);
    }
}