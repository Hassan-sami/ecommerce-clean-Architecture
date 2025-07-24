using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Authentication.Responses;
using MediatR;

namespace ecommerce.Application.Cqrs.Authentication.commands.RefreshToken;

public record RefreshTokenCommand(string accessToken, string refreshToken) : IRequest<Response<JwtResponse>>;