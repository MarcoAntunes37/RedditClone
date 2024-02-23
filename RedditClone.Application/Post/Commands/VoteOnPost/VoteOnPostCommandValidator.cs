namespace RedditClone.Application.Post.Commands.VoteOnPost;

using FluentValidation;

public partial class VoteOnPostCommandValidator : AbstractValidator<VoteOnPostCommand>
{
    public VoteOnPostCommandValidator()
    {
        RuleFor(v => v.PostId)
            .NotNull()
            .WithMessage("Post is invalid");

        RuleFor(v => v.UserId)
            .NotNull()
            .WithMessage("User is invalid");
    }
}