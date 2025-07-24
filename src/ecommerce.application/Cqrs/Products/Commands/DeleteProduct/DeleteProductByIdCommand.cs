using ecommerce.Application.Base;
using MediatR;

namespace ecommerce.Application.Cqrs.Products.Commands.DeleteProduct;

public record DeleteProductByIdCommand(Guid Id) : IRequest<Response<string>>;