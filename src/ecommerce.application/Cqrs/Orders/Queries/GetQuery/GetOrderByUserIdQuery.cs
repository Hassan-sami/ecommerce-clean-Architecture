using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Orders.Responses;
using ecommerce.Application.Cqrs.Products.queries.Reponses;
using MediatR;

namespace ecommerce.Application.Cqrs.Orders.Queries.GetQuery;

public record GetOrderByUserIdQuery(Guid UserId,int page, int size) : IRequest<Response<IReadOnlyList<OrderResponse>>>;