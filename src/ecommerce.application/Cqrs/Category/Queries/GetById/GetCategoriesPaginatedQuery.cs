using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Category.Responses;
using MediatR;

namespace ecommerce.Application.Cqrs.Category.Queries.GetById;

public record GetCategoriesPaginatedQuery(int page,int size) : IRequest<Response<IReadOnlyList<CategoryResponse>>>;
