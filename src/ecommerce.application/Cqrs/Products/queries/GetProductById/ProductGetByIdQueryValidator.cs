using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.Application.Cqrs.Products.queries
{
    public class ProductGetByIdQueryValidator : AbstractValidator<PorductGetByIdQuery>
    {
        public ProductGetByIdQueryValidator()
        {
            RuleFor(x => x.id)
                .NotEmpty().WithMessage("Product ID is required.")
                .Must(id => id != Guid.Empty).WithMessage("Product ID cannot be an empty GUID.");


            
        }

    }
}
