namespace RedditClone.Application.User.Commands.SendPasswordRecoveryEmail;

using FluentValidation;

public partial class SendPasswordRecoveryEmailCommandValidator : AbstractValidator<SendPasswordRecoveryEmailCommand>
{
    public SendPasswordRecoveryEmailCommandValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty()
                .WithMessage("Email cannot be empty")
            .NotNull()
                .WithMessage("Email cannot be null")
            .Length(2, 100)
                .WithMessage("Email must have at least 2 and at maximum 100 characters")
            .EmailAddress()
                .WithMessage("Email must be a valid email");

    }
}