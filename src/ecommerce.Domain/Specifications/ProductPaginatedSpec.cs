using System.Linq.Expressions;
using ecommerce.Domain.Enitities;

namespace ecommerce.Domain.Specifications;

public class ProductPaginatedSpec : BaseSpecification<Product>
{
    public ProductPaginatedSpec(Expression<Func<Product, bool>> criteria) : base(criteria)
    {
    }
    public ProductPaginatedSpec(Expression<Func<Product, bool>> criteria,int skip,int take) : base(criteria)
    {
        ApplyPaging(skip,take);
    }
    public ProductPaginatedSpec(int skip, int take) : base(null) 
    {
        
        ApplyPaging(skip, take);
    }
}