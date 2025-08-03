using FluentValidation;

namespace ecommerce.Application.Cqrs.Products.queries.Proudcts;

public class GetProductsPaginatedValidator  : AbstractValidator<GetProductsPaginatedQuery>
{
    public GetProductsPaginatedValidator()
    {
           RuleFor(x => x.page).NotEmpty().WithMessage("Page cannot be empty");
           RuleFor(x => x.size).NotEmpty().WithMessage("Size cannot be empty");
    }
}