using System.Text.RegularExpressions;
using FluentValidation;

namespace RedditClone.Application.User.Commands.Register;

public partial class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(u => u.FirstName)
            .NotEmpty()
                .WithMessage("Firstname cannot be empty")
            .NotNull()
                .WithMessage("Firstname cannot be null")
            .Length(2, 50)
                .WithMessage("Firstname must have at least 2 and at maximum 50 characters");

        RuleFor(u => u.LastName)
            .NotEmpty()
                .WithMessage("Lastname cannot be empty")
            .NotNull()
                .WithMessage("Lastname cannot be null")
            .Length(2, 50)
                .WithMessage("Lastname must have at least 2 and at maximum 50 characters");

        RuleFor(u => u.Username)
            .NotEmpty()
                .WithMessage("Username cannot be empty")
            .NotNull()
                .WithMessage("Username cannot be null")
            .Length(8, 24)
                .WithMessage("Username must have at least 8 and at maximum 24 characters");

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
                .WithMessage("Password must have at least upper case letter, at least one lower case letter and at least one special character");
    }

    [GeneratedRegex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*\\W).*$")]
    private static partial Regex StrongPassword();
}