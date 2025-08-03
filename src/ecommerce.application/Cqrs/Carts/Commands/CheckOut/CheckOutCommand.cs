using ecommerce.Application.Base;
using MediatR;

namespace ecommerce.Application.Cqrs.Carts.Commands.CheckOut;

public record CheckOutCommand(string Name,  string Street,string City): IRequest<Response<Guid>>;