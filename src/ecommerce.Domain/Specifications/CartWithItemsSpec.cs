using System.Linq.Expressions;
using ecommerce.Domain.Enitities;
using ecommerce.Domain.Interfaces;

namespace ecommerce.Domain.Specifications;

public class CartWithItemsSpec : BaseSpecification<Cart>
{
    public CartWithItemsSpec(Expression<Func<Cart, bool>> criteria,List<Expression<Func<Cart, object>>> includes) : base(criteria)
    {
        Includes?.AddRange(includes);
    }
}