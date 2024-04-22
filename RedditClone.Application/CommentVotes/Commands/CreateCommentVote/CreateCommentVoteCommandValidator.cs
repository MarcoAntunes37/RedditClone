namespace RedditClone.Application.CommentVotes.Commands.CreateCommentVote;

using FluentValidation;

public partial class CreateCommentVoteCommandValidator : AbstractValidator<CreateCommentVoteCommand>
{
    public CreateCommentVoteCommandValidator()
    {
        RuleFor(v => v.CommentId)
            .NotNull()
            .WithMessage("Comment is invalid");

        RuleFor(v => v.UserId)
            .NotNull()
            .WithMessage("User is invalid");
    }
}