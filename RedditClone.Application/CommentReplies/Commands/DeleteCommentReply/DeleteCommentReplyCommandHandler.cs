namespace RedditClone.Application.CommentReplies.Commands.DeleteCommentReply;

using ErrorOr;
using MediatR;
using Serilog;
using RedditClone.Application.Persistence;
using RedditClone.Application.CommentReplies.Commands.DeleteCommentReply;
using RedditClone.Application.CommentReplies.Results.DeleteCommentReplyResults;

public class DeleteCommentReplyCommandHandler(
    ICommentRepository commentRepository)
    : IRequestHandler<DeleteCommentReplyCommand, ErrorOr<DeleteCommentReplyResult>>
{
    private readonly ICommentRepository _commentRepository = commentRepository;

    public async Task<ErrorOr<DeleteCommentReplyResult>> Handle(
        DeleteCommentReplyCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        Log.Information(
            "{@Message}, {@DeleteReplyOnCommentCommand}",
            "Trying delete Reply: {@ReplyId} on Comment: {@CommentId}",
            command,
            command.ReplyId,
            command.CommentId);

        _commentRepository.DeleteCommentReplyById(command.CommentId, command.ReplyId, command.UserId);

        DeleteCommentReplyResult result = new("Reply successfully delete.");

        Log.Information(
            "{@DeleteReplyOnCommentResult}",
            result);

        return result;
    }
}