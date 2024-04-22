namespace RedditClone.Application.User.Commands.PasswordRecoveryCodeValidate;

using FluentValidation;

public partial class PasswordRecoveryCodeValidateCommandValidator : AbstractValidator<PasswordRecoveryCodeValidateCommand>
{
    public PasswordRecoveryCodeValidateCommandValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty()
                .WithMessage("Email cannot be empty")
            .NotNull()
                .WithMessage("Email cannot be null")
            .EmailAddress()
                .WithMessage("Email must be a valid email address");

        RuleFor(u => u.Code)
            .NotEmpty()
                .WithMessage("Code cannot be empty")
            .NotNull()
                .WithMessage("Code cannot be null")
            .Length(6, 6)
                .WithMessage("Code must have 6 characters");
    }
}