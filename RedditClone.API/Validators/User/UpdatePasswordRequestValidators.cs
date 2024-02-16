namespace RedditClone.Application.User.Commands.Register;

using FluentValidation;
using RedditClone.Contracts.UpdatePassword;

public partial class UpdatePasswordRequestValidators : AbstractValidator<UpdatePasswordRequest>
{
    public UpdatePasswordRequestValidators()
    {
        RuleFor(u => u.NewPassword)
            .NotEmpty()
                .WithMessage("New password cannot be empty")
            .NotNull()
                .WithMessage("New password cannot be null");

        RuleFor(u => u.RepeatNewPassword)
            .NotEmpty()
                .WithMessage("Repeat password cannot be empty")
            .NotNull()
                .WithMessage("Repeat password cannot be null")
            .Matches(u => u.NewPassword)
                .WithMessage("Repeat password need to match with new password");;

        RuleFor(u => u.OldPassword)
            .NotEmpty()
                .WithMessage("Old password cannot be empty")
            .NotNull()
                .WithMessage("Old password cannot be null");
    }
}