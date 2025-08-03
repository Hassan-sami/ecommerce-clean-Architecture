using System.Linq.Expressions;
using ecommerce.Domain.Enitities;

namespace ecommerce.Domain.Specifications;

public class CategoryPaginatedSpec : BaseSpecification<Category>
{
    public CategoryPaginatedSpec(Expression<Func<Category, bool>> criteria) : base(criteria)
    {
    }
    public CategoryPaginatedSpec(Expression<Func<Category, bool>> criteria,int skip,int take) : base(criteria)
    {
        ApplyPaging(skip,take);
    }
    public CategoryPaginatedSpec(int skip, int take) : base(null) 
    {
        
        ApplyPaging(skip, take);
    }
}