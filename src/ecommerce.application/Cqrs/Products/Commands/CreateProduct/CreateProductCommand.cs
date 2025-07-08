using MediatR;
using ecommerce.Domain.Enitities;
namespace ecommerce.Application.Cqrs.Products.Commands.CreateProduct
{
    public record CreateProductCommand(
       string Name,
       string Description,
       string? ImageUrl,
       decimal UnitPrice,
       int? UnitsInStock,
       double Star,
       string CatName,
       string CatDescription,
       string? CatImageName
       ) : IRequest<Product>;
}
