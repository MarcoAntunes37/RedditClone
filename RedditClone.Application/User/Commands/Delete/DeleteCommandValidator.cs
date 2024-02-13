using FluentValidation;

namespace RedditClone.Application.User.Commands.Delete;

public partial class DeleteCommandValidator : AbstractValidator<DeleteCommand>
{
    public DeleteCommandValidator()
    {
        RuleFor(u => u.UserId)
            .NotNull()
                .WithMessage("An error occurred invalid user");
    }
}