namespace RedditClone.Application.User.Commands.PasswordRecoveryNewPasswordCommandValidator;

using FluentValidation;
using System.Text.RegularExpressions;
using RedditClone.Application.User.Commands.PasswordRecoveryNewPassword;

public partial class PasswordRecoveryNewPasswordCommandValidator : AbstractValidator<PasswordRecoveryNewPasswordCommand>
{
    [GeneratedRegex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*\\W).*$")]
    private static partial Regex StrongPassword();
    public PasswordRecoveryNewPasswordCommandValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty()
                .WithMessage("Email cannot be empty")
            .NotNull()
                .WithMessage("Email cannot be null")
            .EmailAddress()
                .WithMessage("Email must be a valid email address");

        Regex strongPassword = StrongPassword();

        RuleFor(u => u.NewPassword)
            .NotEmpty()
                .WithMessage("Password cannot be empty")
            .NotNull()
                .WithMessage("Password cannot be null")
            .Length(8, 100)
                .WithMessage("Password must have at least 8 and at maximum 100 characters")
            .Matches(StrongPassword())
                .WithMessage("Password must have at a number, a upper case letter, a one lower case letter and a special character");

        RuleFor(u => u.ConfirmPassword)
            .Equal(u => u.NewPassword)
                .WithMessage("Confirm password must match with new password");
    }
}