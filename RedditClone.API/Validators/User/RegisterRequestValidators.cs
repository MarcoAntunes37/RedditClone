namespace RedditClone.Application.User.Commands.Register;

using FluentValidation;
using RedditClone.Contracts.Register;

public partial class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(u => u.Password)
            .NotEmpty()
                .WithMessage("Password cannot be empty")
            .NotNull()
                .WithMessage("Password cannot be null")
            .Matches(u => u.RepeatPassword)
                .WithMessage("Repeat password need to match with password");
    }
}