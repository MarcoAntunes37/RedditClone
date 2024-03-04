namespace RedditClone.Application.Community.Commands.DeleteVoteOnReply;

using FluentValidation;

public partial class DeleteVoteOnReplyCommandValidator : AbstractValidator<DeleteVoteOnReplyCommand>
{
    public DeleteVoteOnReplyCommandValidator()
    {
        RuleFor(u => u.UserId)
            .NotNull()
                .WithMessage("An error occurred invalid user");

        RuleFor(u => u.VoteId)
            .NotNull()
                .WithMessage("An error occurred invalid vote");

        RuleFor(u => u.CommentId)
        .NotNull()
            .WithMessage("An error occurred invalid comment");
    }
}