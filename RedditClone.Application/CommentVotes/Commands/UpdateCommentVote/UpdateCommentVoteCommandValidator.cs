namespace RedditClone.Application.CommentVotes.Commands.UpdateCommentVote;

using FluentValidation;

public partial class UpdateCommentVoteCommandValidator : AbstractValidator<UpdateCommentVoteCommand>
{
    public UpdateCommentVoteCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotNull()
                .WithMessage("An error occurred invalid user");

        RuleFor(c => c.CommentId)
        .NotNull()
            .WithMessage("An error occurred invalid comment");

        RuleFor(c => c.IsVoted)
            .NotNull()
                .WithMessage("Voted cannot be null");
    }
}