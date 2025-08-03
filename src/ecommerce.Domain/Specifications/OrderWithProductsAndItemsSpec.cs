using System.Linq.Expressions;
using ecommerce.Domain.Enitities;

namespace ecommerce.Domain.Specifications;

public class OrderWithProductsAndItemsSpec : BaseSpecification<Order>
{
    public OrderWithProductsAndItemsSpec(Expression<Func<Order, bool>> criteria) : base(criteria)
    {
        
    }
    public OrderWithProductsAndItemsSpec(Expression<Func<Order, bool>> criteria,int take, int skip) : base(criteria)
    {
        ApplyPaging(skip, take);
        Includes?.Add(or => or.Items.Select(i => i.Product).FirstOrDefault());
        Includes?.Add(or => or.Items);
        
        
    }
}