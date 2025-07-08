using FluentValidation;

namespace ecommerce.Application.Cqrs.Products.Commands.CreateProduct
{
    class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
            RuleFor(x => x.UnitsInStock)
                .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative.");
        }
    }
}
