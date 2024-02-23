namespace RedditClone.Application.Comment.Commands.VoteOnComment;

using FluentValidation;

public partial class VoteOnCommentValidator : AbstractValidator<VoteOnCommentCommand>
{
    public VoteOnCommentValidator()
    {
        RuleFor(v => v.CommentId)
            .NotNull()
            .WithMessage("Comment is invalid");

        RuleFor(v => v.UserId)
            .NotNull()
            .WithMessage("User is invalid");
    }
}