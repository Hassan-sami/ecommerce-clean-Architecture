
using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Products.queries.Reponses;
using MediatR;

namespace ecommerce.Application.Cqrs.Orders.Queries.GetQuery;

public record GetTopSellersQuery() : IRequest<Response<IReadOnlyList<ProductResponse>>>;