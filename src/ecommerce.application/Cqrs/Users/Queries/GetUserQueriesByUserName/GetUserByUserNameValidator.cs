using FluentValidation;

namespace ecommerce.Application.Cqrs.Users.Queries.GetUserQueriesByUserName;

public class GetUserByUserNameValidator : AbstractValidator<GetUserByUserNameQuery>
{
    public GetUserByUserNameValidator()
    {
        RuleFor(q => q.UserName).NotNull().NotEmpty().WithMessage("username cannot be empty");
    }
}