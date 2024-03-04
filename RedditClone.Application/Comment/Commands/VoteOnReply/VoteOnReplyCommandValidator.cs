namespace RedditClone.Application.Comment.Commands.VoteOnReply;

using FluentValidation;

public partial class VoteOnReplyCommandValidator : AbstractValidator<VoteOnReplyCommand>
{
    public VoteOnReplyCommandValidator()
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