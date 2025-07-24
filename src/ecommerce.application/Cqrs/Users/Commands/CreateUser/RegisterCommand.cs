using System.Windows.Input;
using ecommerce.Application.Base;
using MediatR;

namespace ecommerce.Application.Cqrs.Users.Commands.CreateUser;

public record RegisterCommand(string FirstName, 
    string LastName,
    string Email, string Password,
    string ConfirmPassword,
    List<string>? Phones) : IRequest<Response<string>>;