namespace RedditClone.Application.Comment.Commands.VoteOnComment;

using FluentValidation;

public partial class VoteOnCommentCommandValidator : AbstractValidator<VoteOnCommentCommand>
{
    public VoteOnCommentCommandValidator()
    {
        RuleFor(v => v.CommentId)
            .NotNull()
            .WithMessage("Comment is invalid");

        RuleFor(v => v.UserId)
            .NotNull()
            .WithMessage("User is invalid");
    }
}