using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Products.queries.Reponses;
using MediatR;

namespace ecommerce.Application.Cqrs.Products.Commands.UpdateProduct;

public record UpdateProductCommand 
    (
        Guid Id,
        string Name,
        string Description,
        string? ImageUrl,
        decimal UnitPrice,
        int? UnitsInStock,
        double Star,
        Guid CategoryId
    ): IRequest<Response<ProductResponse>>;
