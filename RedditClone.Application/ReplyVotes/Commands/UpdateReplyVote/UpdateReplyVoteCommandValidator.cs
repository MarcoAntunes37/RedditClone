namespace RedditClone.Application.ReplyVotes.Commands.UpdateReplyVote;

using FluentValidation;

public partial class UpdateReplyVoteCommandValidator : AbstractValidator<UpdateReplyVoteCommand>
{
    public UpdateReplyVoteCommandValidator()
    {
        RuleFor(v => v.CommentId)
            .NotNull()
            .WithMessage("Comment is invalid");

        RuleFor(v => v.ReplyId)
            .NotNull()
            .WithMessage("Comment is invalid");

        RuleFor(v => v.UserId)
            .NotNull()
            .WithMessage("User is invalid");
    }
}