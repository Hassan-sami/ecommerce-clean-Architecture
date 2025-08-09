using ecommerce.Domain.Enitities;

namespace ecommerce.Domain.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<List<Product>?> GetBestSellers();
}