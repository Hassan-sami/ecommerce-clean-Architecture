using ecommerce.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IReadOnlyList<Product>> GetProductsAsync();

        ValueTask<Product> GetProdctByIdAsync(Guid id);

        Task<IReadOnlyList<Product>> GetProductByNameAsync(string productName);


        Task<Product?> GetProductByIdWithCategoryAsync(Guid productId);
        
        ValueTask<IReadOnlyList<Product>> GetProductsPaginatedAsync(int skip, int take);
    }
}
