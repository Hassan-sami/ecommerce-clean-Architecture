using ecommerce.Domain.Enitities;
using ecommerce.Domain.Interfaces;
using ecommerce.Domain.Specifications;
using ecommerce.infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ecommerce.infra.Repos
{
    public class Productrepository : Repository<Product>, IProductRepository
    {
        
        public Productrepository(AppDbContext dbContext) : base(dbContext)
        {
            
        }

        public async ValueTask<Product?> GetProdctByIdAsync(Guid id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await GetAsync(disableTracking: true);
        }

        public async Task<Product?> GetProductByIdWithCategoryAsync(Guid productId)
        {
            var spec = new ProductWithCategorySpec(productId);
            return (await GetAsync(spec)).FirstOrDefault();
        }

        public async ValueTask<IReadOnlyList<Product>> GetProductsPaginatedAsync(int skip, int take)
        {
            var spec = new ProductPaginatedSpec(skip, take);
            return await GetAsync(spec);
        }

        public async Task<IReadOnlyList<Product>> GetProductByNameAsync(string productName)
        {
            var spec = new ProductWithCategorySpec(productName);

            return await GetAsync(spec);
        }

        
    }
}
