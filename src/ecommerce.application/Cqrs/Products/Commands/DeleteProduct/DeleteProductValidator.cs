using FluentValidation;

namespace ecommerce.Application.Cqrs.Products.Commands.DeleteProduct;

public class DeleteProductValidator : AbstractValidator<DeleteProductByIdCommand>
{
    public DeleteProductValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Product ID is required.")
            .Must(id => id != Guid.Empty).WithMessage("Product ID cannot be an empty GUID.");
        
    }
}