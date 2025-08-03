using System.Windows.Input;
using ecommerce.Application.Base;
using MediatR;

namespace ecommerce.Application.Cqrs.Carts.Commands.AddItem;

public record AddItemCommand(Guid ProductId,int Quantity) : IRequest<Response<Guid>>;