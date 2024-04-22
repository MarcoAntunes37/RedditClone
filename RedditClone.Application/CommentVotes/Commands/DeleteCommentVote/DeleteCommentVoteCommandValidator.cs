namespace RedditClone.Application.CommentVotes.Commands.DeleteCommentVote;

using FluentValidation;

public partial class DeleteCommentVoteCommandValidator : AbstractValidator<DeleteCommentVoteCommand>
{
    public DeleteCommentVoteCommandValidator()
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