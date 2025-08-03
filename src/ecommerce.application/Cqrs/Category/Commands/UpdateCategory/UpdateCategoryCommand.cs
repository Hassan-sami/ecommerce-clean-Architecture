using ecommerce.Application.Base;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ecommerce.Application.Cqrs.Category.Commands.UpdateCategory;

public record UpdateCategoryCommand(Guid Id,string Name, string Description, IFormFile FormFile) : IRequest<Response<string>>;
