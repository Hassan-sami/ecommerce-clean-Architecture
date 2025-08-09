using ecommerce.Application.Cqrs.Orders.Responses;
using ecommerce.Domain.Enitities;
using ecommerce.Domain.Interfaces;
using ecommerce.infra.Context;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.infra.Repos;

public class OrderRespository : Repository<Order>, IOrderRepository
{
    public OrderRespository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Product>?> GetBestSellers()
    {
        var result = await _dbContext.Set<Product>()
            .FromSqlRaw("EXEC GetBestSellers @TopN = {0}", 10).ToListAsync();
        return result;
    }
}