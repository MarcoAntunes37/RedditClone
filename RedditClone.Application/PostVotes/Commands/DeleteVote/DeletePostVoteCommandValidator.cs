namespace RedditClone.Application.PostVotes.Commands.DeleteVote;

using FluentValidation;

public partial class DeletePostVoteCommandValidator : AbstractValidator<DeletePostVoteCommand>
{
    public DeletePostVoteCommandValidator()
    {
        RuleFor(v => v.VoteId)
            .NotNull()
            .WithMessage("Vote is invalid");
        RuleFor(v => v.PostId)
            .NotNull()
            .WithMessage("Post is invalid");

        RuleFor(v => v.UserId)
            .NotNull()
            .WithMessage("User is invalid");
    }
}