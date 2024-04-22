namespace RedditClone.Application.CommentReplies.Commands.DeleteCommentReply;

using FluentValidation;
using RedditClone.Application.CommentReplies.Commands.DeleteCommentReply;

public partial class DeleteCommentReplyCommandValidator : AbstractValidator<DeleteCommentReplyCommand>
{
    public DeleteCommentReplyCommandValidator()
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