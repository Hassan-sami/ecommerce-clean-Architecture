using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Authentication.Responses;
using MediatR;

namespace ecommerce.Application.Cqrs.Users.Queries.SignInQueries;

public record SignInQuery(string Email, string Password) : IRequest<Response<SignInResponse>>;
