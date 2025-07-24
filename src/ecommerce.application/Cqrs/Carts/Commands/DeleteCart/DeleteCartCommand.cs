using ecommerce.Application.Base;
using MediatR;

namespace ecommerce.Application.Cqrs.Carts.Commands.DeleteCart;

public record DeleteCartCommand(Guid Id) : IRequest<Response<string>>;