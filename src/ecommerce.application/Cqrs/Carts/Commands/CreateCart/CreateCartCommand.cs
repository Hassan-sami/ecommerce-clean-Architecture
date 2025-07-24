using ecommerce.Application.Base;
using ecommerce.Domain.Enitities;
using MediatR;

namespace ecommerce.Application.Cqrs.Carts.Commands.CreateCart;

public record CreateCartCommand(Guid UserId, List<CartItem> CartItems) : IRequest<Response<string>>;