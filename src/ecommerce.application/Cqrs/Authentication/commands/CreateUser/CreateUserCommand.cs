using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Authentication.Responses;
using ecommerce.Domain.Enitities.Identities;
using MediatR;

namespace ecommerce.Application.Cqrs.Authentication.commands.CreateUser;

public record CreateUserCommand(
    string Email,
    string firstName,
    string lastName
) : IRequest<Response<AppUserCreated>>;
