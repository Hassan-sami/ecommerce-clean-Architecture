using FluentValidation;

namespace ecommerce.Application.Cqrs.Users.Queries.GetUserQueriesByUserName;

public class GetUserByEmailValidator : AbstractValidator<GetUserByEmailQuery>
{
    public GetUserByEmailValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("email is required").EmailAddress().WithMessage("email address is invalid");
    }
}