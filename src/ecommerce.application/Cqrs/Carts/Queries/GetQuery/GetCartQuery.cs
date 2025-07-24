using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Carts.Reponses;
using ecommerce.Domain.Enitities;
using MediatR;

namespace ecommerce.Application.Cqrs.Carts.Queries.GetQuery;

public record GetCartQuery(Guid Id) : IRequest<Response<CartResponse>>;