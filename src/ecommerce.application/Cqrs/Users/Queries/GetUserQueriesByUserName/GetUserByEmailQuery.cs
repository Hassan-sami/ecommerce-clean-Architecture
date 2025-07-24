using ecommerce.Application.Base;
using ecommerce.Domain.Enitities.Identities;
using MediatR;

namespace ecommerce.Application.Cqrs.Users.Queries.GetUserQueriesByUserName;

public record GetUserByEmailQuery(string Email) : IRequest<Response<AppUser>>;