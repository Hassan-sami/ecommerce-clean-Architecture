using System.Security.Claims;
using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Authentication.Responses;
using MediatR;

namespace ecommerce.Application.Cqrs.Authentication.queries.SignInQueries;

public record SignInQueryWithClaimPrinciple(ClaimsPrincipal User,string UserId) : IRequest<Response<SignInResponse>>;
