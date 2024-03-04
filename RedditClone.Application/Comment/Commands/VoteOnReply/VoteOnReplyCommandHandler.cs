namespace RedditClone.Application.Comment.Commands.VoteOnReply;

using FluentValidation;
using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Application.Comment.Results.VoteOnReplyResult;

public class VoteOnReplyCommandHandler
    : IRequestHandler<VoteOnReplyCommand, VoteOnReplyResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<VoteOnReplyCommand> _validator;

    public VoteOnReplyCommandHandler(
        ICommentRepository commentRepository,
        IValidator<VoteOnReplyCommand> validator)
    {
        _commentRepository = commentRepository;
        _validator = validator;
    }

    public async Task<VoteOnReplyResult> Handle(VoteOnReplyCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(command);

        _commentRepository.AddReplyVote(command.CommentId, command.ReplyId, command.UserId, command.IsVoted);

        return new VoteOnReplyResult(
            "Comment reply voted successfully"
        );
    }
}