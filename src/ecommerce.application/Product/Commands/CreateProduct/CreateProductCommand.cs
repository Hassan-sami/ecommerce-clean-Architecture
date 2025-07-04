using MediatR;
using ecommerce.Domain.Enitities;
namespace ecommerce.Application.Product.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<Domain.Enitities.Product>
    {
        public Domain.Enitities.Product Product { get; private set; }
        public CreateProductCommand(Domain.Enitities.Product product)
        {
            Product = product;
        }
    }
}
