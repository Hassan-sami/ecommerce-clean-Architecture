using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Category.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ecommerce.Application.Cqrs.Category.Commands.CreateCategory;

public sealed record CreateCategoryCommand(string Name,string Description, IFormFile File)
    : IRequest<Response<CategoryResponse>>;