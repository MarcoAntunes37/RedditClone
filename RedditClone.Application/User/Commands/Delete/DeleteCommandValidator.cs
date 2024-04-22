namespace RedditClone.Application.User.Commands.Delete;

using FluentValidation;

public partial class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(u => u.UserId)
            .NotNull()
                .WithMessage("An error occurred invalid user");
    }
}