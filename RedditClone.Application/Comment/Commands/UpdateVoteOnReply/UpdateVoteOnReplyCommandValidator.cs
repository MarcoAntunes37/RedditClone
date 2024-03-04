namespace RedditClone.Application.Comment.Commands.UpdateVoteOnReply;

using FluentValidation;

public partial class UpdateVoteOnReplyCommandValidator : AbstractValidator<UpdateVoteOnReplyCommand>
{
    public UpdateVoteOnReplyCommandValidator()
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