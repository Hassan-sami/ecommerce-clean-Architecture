using ecommerce.Application.Base;
using MediatR;

namespace ecommerce.Application.Cqrs.Carts.Commands.RemoveItem;

public record RemoveBasketProductItemCommand(Guid ProductId) : IRequest<Response<Guid>>;