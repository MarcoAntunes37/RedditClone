namespace RedditClone.Application.Community.Commands.DeleteReplyOnComment;

using FluentValidation;

public partial class DeleteReplyOnCommentCommandValidator : AbstractValidator<DeleteReplyOnCommentCommand>
{
    public DeleteReplyOnCommentCommandValidator()
    {
        RuleFor(u => u.UserId)
            .NotNull()
                .WithMessage("An error occurred invalid user");

        RuleFor(u => u.ReplyId)
            .NotNull()
                .WithMessage("An error occurred invalid vote");

        RuleFor(u => u.CommentId)
        .NotNull()
            .WithMessage("An error occurred invalid comment");
    }
}