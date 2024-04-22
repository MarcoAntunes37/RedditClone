namespace RedditClone.Application.PostVotes.Commands.UpdateVote;

using FluentValidation;

public partial class UpdatePostVoteCommandValidator : AbstractValidator<UpdatePostVoteCommand>
{
    public UpdatePostVoteCommandValidator()
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