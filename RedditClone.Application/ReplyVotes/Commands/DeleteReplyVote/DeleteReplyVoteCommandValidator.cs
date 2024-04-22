namespace RedditClone.Application.ReplyVotes.Commands.DeleteReplyVote;

using FluentValidation;

public partial class DeleteReplyVoteCommandValidator : AbstractValidator<DeleteReplyVoteCommand>
{
    public DeleteReplyVoteCommandValidator()
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