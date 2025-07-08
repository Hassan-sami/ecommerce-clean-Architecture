using ecommerce.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.Domain.Specifications
{
    public class ProductWithCategorySpec : BaseSpecification<Product>
    {
        public ProductWithCategorySpec(string productName) : base(p => p.Name.ToLower() == productName.ToLower())
        {
            base.AddInclude(p => p.Category);
        }
        public ProductWithCategorySpec(Guid  productId) : base(p => p.Id == productId)
        {
            base.AddInclude(p => p.Category);
        }
        public ProductWithCategorySpec(): base(null)
        {
            AddInclude(p => p.Category);
        }
    }
}
