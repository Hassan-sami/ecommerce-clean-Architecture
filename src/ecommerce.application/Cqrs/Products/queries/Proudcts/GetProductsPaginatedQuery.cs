using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Products.queries.Reponses;
using MediatR;

namespace ecommerce.Application.Cqrs.Products.queries.Proudcts;

public record GetProductsPaginatedQuery(int page, int size):IRequest<Response<IReadOnlyList<ProductResponse>>>;
