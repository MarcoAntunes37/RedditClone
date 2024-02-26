namespace RedditClone.Application.Community.Commands.DeleteComment;

using FluentValidation;

public partial class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
{
    public DeleteCommentCommandValidator()
    {
        RuleFor(u => u.UserId)
            .NotNull()
                .WithMessage("An error occurred invalid user");

        RuleFor(u => u.CommentId)
        .NotNull()
            .WithMessage("An error occurred invalid comment");
    }
}