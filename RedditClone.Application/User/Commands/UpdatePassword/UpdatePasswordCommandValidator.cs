using System.Text.RegularExpressions;
using FluentValidation;
using RedditClone.Application.User.Commands.UpdatePassword;

namespace RedditClone.Application.User.Commands.Update;

public partial class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
{
    [GeneratedRegex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*\\W).*$")]
    private static partial Regex StrongPassword();
    public UpdatePasswordCommandValidator()
    {
        Regex strongPassword = StrongPassword();

        RuleFor(u => u.UserId)
            .NotEmpty()
                .WithMessage("UserId cannot be empty")
            .NotNull()
                .WithMessage("UserId cannot be null");

        RuleFor(u => u.NewPassword)
            .NotEmpty()
                .WithMessage("Password cannot be empty")
            .NotNull()
                .WithMessage("Password cannot be null")
            .Length(8, 100)
                .WithMessage("Password must have at least 8 and at maximum 100 characters")
            .Matches(StrongPassword())
                .WithMessage("Password must have at a number, a upper case letter, a one lower case letter and a special character");

        RuleFor(u => u.MatchPassword)
            .Equal(u => u.NewPassword)
                .WithMessage("Confirm password must match with new password");

        RuleFor(u => u.OldPassword)
            .NotEmpty()
                .WithMessage("Old password cannot be empty")
            .NotNull()
                .WithMessage("Old password cannot be null");
    }
}