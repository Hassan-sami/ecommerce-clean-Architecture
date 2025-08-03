using System.Linq.Expressions;
using ecommerce.Application.Interfaces;
using ecommerce.Domain.Enitities;
using ecommerce.Domain.Interfaces;

namespace ecommerce.Application.Services
{
    public class PorductService : IProductService
    {
        private readonly IProductRepository repository;

        public PorductService(IProductRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Product> Create(Product product)
        {
            return await repository.AddAsync(product);    
        }

        public async ValueTask<Product?> GetProductByIdAsync(Guid id)
        {
            
            return await repository.GetProdctByIdAsync(id);
        }

        public async ValueTask<Product?> GetProdctByIdWithCategoryAsync(Guid id)
        {
            return await repository.GetProductByIdWithCategoryAsync(id);
        }

        public async ValueTask<IReadOnlyList<Product>> GetAllProducts()
        {
            return await repository.GetProductsAsync();
        }

        public async ValueTask<IReadOnlyList<Product>> GetProductsPaginatedAsync(int skip, int take)
        {
            return await repository.GetProductsPaginatedAsync(skip, take);
        }

        public async ValueTask<Product> DeleteProductByIdAsync(Guid id)
        {
            var product = await repository.GetProdctByIdAsync(id);
            if (product == null)
                return null;
            await repository.DeleteAsync(product);
            return product;
        }

        public async ValueTask<Product> Update(Product product)
        {
            await repository.UpdateAsync(product);
            var includes = new List<Expression<Func<Product, object>>>()
            {   
                (p => p.Category)
            };
            return (await repository.GetAsync(p => p.Id == product.Id,null,
                includes,true)).FirstOrDefault();
        }
    }
}
