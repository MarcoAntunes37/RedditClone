namespace RedditClone.Application.User.Queries.Login;

using FluentValidation;

public partial class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty()
                .WithMessage("Email cannot be empty")
            .NotNull()
                .WithMessage("Email cannot be null")
            .EmailAddress()
                .WithMessage("Email must be a valid email address");

        RuleFor(u => u.Password)
            .NotEmpty()
                .WithMessage("Password cannot be empty")
            .NotNull()
                .WithMessage("Password cannot be null");
    }
}