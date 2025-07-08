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

        public async ValueTask<Product> GetProductByIdAsync(Guid id)
        {
            
            return await repository.GetProdctByIdAsync(id);
        }
    }
}
