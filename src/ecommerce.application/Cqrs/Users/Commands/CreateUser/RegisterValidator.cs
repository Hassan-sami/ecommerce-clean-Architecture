using FluentValidation;

namespace ecommerce.Application.Cqrs.Users.Commands.CreateUser;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name cannot be empty");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name cannot be empty");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty").EmailAddress().WithMessage("Email address is not valid");
        RuleFor(x=> x.ConfirmPassword).NotEmpty().WithMessage("Confirm password cannot be empty");
    }
}