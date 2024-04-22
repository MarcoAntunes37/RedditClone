namespace RedditClone.Application.ReplyVotes.Commands.CreateReplyVote;

using FluentValidation;

public partial class CreateReplyVoteCommandValidator : AbstractValidator<CreateReplyVoteCommand>
{
    public CreateReplyVoteCommandValidator()
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