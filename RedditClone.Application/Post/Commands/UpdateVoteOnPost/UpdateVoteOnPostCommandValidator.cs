namespace RedditClone.Application.Post.Commands.UpdateVoteOnPost;

using FluentValidation;

public partial class UpdateVoteOnPostCommandValidator : AbstractValidator<UpdateVoteOnPostCommand>
{
    public UpdateVoteOnPostCommandValidator()
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