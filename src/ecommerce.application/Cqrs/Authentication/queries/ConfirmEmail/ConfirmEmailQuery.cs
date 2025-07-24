using ecommerce.Application.Base;
using MediatR;

namespace ecommerce.Application.Cqrs.Authentication.queries.ConfirmEmail;

public record ConfirmEmailQuery(string UserId, string code) : IRequest<Response<string>>;