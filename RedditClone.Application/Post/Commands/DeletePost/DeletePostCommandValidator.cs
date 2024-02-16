using FluentValidation;

namespace RedditClone.Application.Post.Commands.DeletePost;

public partial class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
{
    public DeletePostCommandValidator()
    {
        RuleFor(p => p.PostId)
            .NotNull()
                .WithMessage("PostId cannot be null");

        RuleFor(p => p.UserId)
            .NotNull()
                .WithMessage("UserId cannot be null");

    }
}