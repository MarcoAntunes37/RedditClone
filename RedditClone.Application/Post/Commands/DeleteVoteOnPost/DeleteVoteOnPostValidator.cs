namespace RedditClone.Application.Post.Commands.DeleteVoteOnPost;

using FluentValidation;

public partial class DeleteVoteOnPostCommandValidator : AbstractValidator<DeleteVoteOnPostCommand>
{
    public DeleteVoteOnPostCommandValidator()
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