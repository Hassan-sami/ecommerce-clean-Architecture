using ecommerce.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.Application.Interfaces
{
    public interface IProductService
    {
        Task<Product> Create(Product product);
        ValueTask<Product?> GetProductByIdAsync(Guid id);
        ValueTask<Product?> GetProdctByIdWithCategoryAsync(Guid id);
        ValueTask<IReadOnlyList<Product>> GetAllProducts();
        ValueTask<IReadOnlyList<Product>> GetProductsPaginatedAsync(int skip, int take);
        ValueTask<Product> DeleteProductByIdAsync(Guid id);
        ValueTask<Product> Update(Product product);
    }

}
