using ecommerce.Domain.Enitities;
using ecommerce.Domain.Interfaces;

namespace ecommerce.Application.Interfaces;

public interface IOrderService
{
    Task<IReadOnlyList<Order>> getOrders(ISpecification<Order> specification);
    Task<IReadOnlyList<Product>?> GetBestProducts();
}