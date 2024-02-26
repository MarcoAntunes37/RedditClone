namespace RedditClone.Application.Community.Commands.DeleteComment;

using FluentValidation;
using RedditClone.Application.Community.Commands.DeleteVoteOnComment;

public partial class DeleteVoteOnCommentCommandValidator : AbstractValidator<DeleteVoteOnCommentCommand>
{
    public DeleteVoteOnCommentCommandValidator()
    {
        RuleFor(u => u.UserId)
            .NotNull()
                .WithMessage("An error occurred invalid user");

        RuleFor(u => u.VoteId)
            .NotNull()
                .WithMessage("An error occurred invalid vote");

        RuleFor(u => u.CommentId)
        .NotNull()
            .WithMessage("An error occurred invalid comment");
    }
}