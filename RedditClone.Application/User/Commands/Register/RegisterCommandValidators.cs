namespace RedditClone.Application.User.Commands.Register;

using FluentValidation;
using System.Text.RegularExpressions;

public partial class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    [GeneratedRegex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*\\W).*$")]
    private static partial Regex StrongPassword();
    public RegisterCommandValidator()
    {
        RuleFor(u => u.Firstname)
            .NotEmpty()
                .WithMessage("Firstname cannot be empty")
            .NotNull()
                .WithMessage("Firstname cannot be null")
            .Length(2, 100)
                .WithMessage("Firstname must have at least 2 and at maximum 100 characters");

        RuleFor(u => u.Lastname)
            .NotEmpty()
                .WithMessage("Lastname cannot be empty")
            .NotNull()
                .WithMessage("Lastname cannot be null")
            .Length(2, 100)
                .WithMessage("Lastname must have at least 2 and at maximum 100 characters");

        RuleFor(u => u.Username)
            .NotEmpty()
                .WithMessage("Username cannot be empty")
            .NotNull()
                .WithMessage("Username cannot be null")
            .Length(8, 100)
                .WithMessage("Username must have at least 8 and at maximum 100 characters");

        RuleFor(u => u.Email)
            .NotEmpty()
                .WithMessage("Email cannot be empty")
            .NotNull()
                .WithMessage("Email cannot be null")
            .EmailAddress()
                .WithMessage("Email must be a valid email address");

        Regex strongPassword = StrongPassword();

        RuleFor(u => u.Password)
            .NotEmpty()
                .WithMessage("Password cannot be empty")
            .NotNull()
                .WithMessage("Password cannot be null")
            .Length(8, 100)
                .WithMessage("Password must have at least 8 and at maximum 100 characters")
            .Matches(StrongPassword())
                .WithMessage("Password must have at a number, a upper case letter, a one lower case letter and a special character");

        RuleFor(u => u.RepeatPassword)
            .Equal(u => u.Password)
                .WithMessage("Confirm password must match with new password");
    }
}