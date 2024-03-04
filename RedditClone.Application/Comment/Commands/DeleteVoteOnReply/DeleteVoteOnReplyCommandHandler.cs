namespace RedditClone.Application.Community.Commands.DeleteVoteOnReply;

using FluentValidation;
using MediatR;
using RedditClone.Application.Community.Results.DeleteVoteOnReplyResult;
using RedditClone.Application.Persistence;

public class DeleteVoteOnReplyCommandHandler :
    IRequestHandler<DeleteVoteOnReplyCommand, DeleteVoteOnReplyResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<DeleteVoteOnReplyCommand> _validator;

    public DeleteVoteOnReplyCommandHandler(
        ICommentRepository commentRepository,
        IValidator<DeleteVoteOnReplyCommand> validator)
    {
        _commentRepository = commentRepository;
        _validator = validator;
    }

    public async Task<DeleteVoteOnReplyResult> Handle(DeleteVoteOnReplyCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(command);

        _commentRepository.DeleteReplyVoteById(command.CommentId, command.ReplyId, command.VoteId, command.UserId);

        return new DeleteVoteOnReplyResult(
            "Vote on reply Deleted."
        );
    }
}