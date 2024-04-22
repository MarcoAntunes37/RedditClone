namespace RedditClone.Application.PostVotes.Commands.CreateVote;

using FluentValidation;

public partial class CreatePostVoteCommandValidator : AbstractValidator<CreatePostVoteCommand>
{
    public CreatePostVoteCommandValidator()
    {
        RuleFor(v => v.PostId)
            .NotNull()
            .WithMessage("Post is invalid");

        RuleFor(v => v.UserId)
            .NotNull()
            .WithMessage("User is invalid");
    }
}