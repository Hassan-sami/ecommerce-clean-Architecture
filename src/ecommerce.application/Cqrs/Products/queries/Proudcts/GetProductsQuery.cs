using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Products.queries.Reponses;
using ecommerce.Domain.Enitities;
using MediatR;

namespace ecommerce.Application.Cqrs.Products.queries.Proudcts;

public record GetProductsQuery() : IRequest<Response<IReadOnlyList<ProductResponse>>>;
