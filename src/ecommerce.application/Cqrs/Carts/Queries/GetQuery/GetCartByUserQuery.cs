
using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Carts.Reponses;
using MediatR;

namespace ecommerce.Application.Cqrs.Carts.Queries.GetQuery;

public record GetCartByUserQuery() : IRequest<Response<CartResponse>>;