using ecommerce.Application.Base;
using MediatR;

namespace ecommerce.Application.Cqrs.Category.Commands.DeleteCategory;

public record DeleteCategoryCommand(Guid Id) : IRequest<Response<string>>;