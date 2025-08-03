using FluentValidation;

namespace ecommerce.Application.Cqrs.Orders.Queries.GetQuery;

public class GetOrderByUserIdValidator : AbstractValidator<GetOrderByUserIdQuery>
{
    public GetOrderByUserIdValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().NotNull();
        RuleFor(x => x.page).NotEmpty().GreaterThan(0);
        RuleFor(x => x.size).NotEmpty().GreaterThan(0);
    }
}