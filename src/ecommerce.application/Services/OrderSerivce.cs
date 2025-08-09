using ecommerce.Application.Interfaces;
using ecommerce.Domain.Enitities;
using ecommerce.Domain.Interfaces;

namespace ecommerce.Application.Services;

public class OrderSerivce : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderSerivce(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<IReadOnlyList<Order>> getOrders(ISpecification<Order> specification)
    {
        return await _orderRepository.GetAsync(specification);
    }

    public async Task<IReadOnlyList<Product>> GetBestProducts()
    {
        return (await _orderRepository.GetBestSellers())?.AsReadOnly();
    }
}