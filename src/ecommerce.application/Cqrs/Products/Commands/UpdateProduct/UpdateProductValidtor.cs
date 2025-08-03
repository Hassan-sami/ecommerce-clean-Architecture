using FluentValidation;

namespace ecommerce.Application.Cqrs.Products.Commands.UpdateProduct;

public class UpdateProductValidtor : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidtor()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Product ID is required.")
            .Must(id => id != Guid.Empty).WithMessage("Product ID cannot be an empty GUID.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Product Name is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Product Description is required.");
        RuleFor(x => x.UnitPrice).GreaterThan(0).WithMessage("Product Unit Price must be greater than 0.");
        RuleFor(x => x.UnitsInStock).NotEmpty().WithMessage("Product Units In Stock is required.").GreaterThan(0).WithMessage("Product Units In Stock must be greater than 0.");
    }
}