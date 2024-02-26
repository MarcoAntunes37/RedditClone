namespace RedditClone.Application.Comment.Commands.UpdateComment;

using FluentValidation;
using RedditClone.Application.Comment.Commands.UpdateVoteOnComment;

public partial class UpdateVoteOnCommentCommandValidator : AbstractValidator<UpdateVoteOnCommentCommand>
{
    public UpdateVoteOnCommentCommandValidator()
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