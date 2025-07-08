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

        public async ValueTask<Product> GetProdctByIdAsync(Guid id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task<IReadOnlyList<Product>> GetProducsAsync()
        {
            return await GetAllAsync();
        }

        public async Task<Product> GetProductByIdWithCategoryAsync(Guid productId)
        {
            var spec = new ProductWithCategorySpec(productId);
            return (await GetAsync(spec)).FirstOrDefault();
        }

        public async Task<IReadOnlyList<Product>> GetProductByNameAsync(string productName)
        {
            var spec = new ProductWithCategorySpec(productName);

            return await GetAsync(spec);
        }

        
    }
}
