namespace RedditClone.Application.CommentReplies.Commands.DeleteCommentReply;

using ErrorOr;
using MediatR;
using Serilog;
using RedditClone.Application.Persistence;
using RedditClone.Application.CommentReplies.Results.DeleteCommentReplyResults;
using RedditClone.Domain.Common.Errors;

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

        var comment = _commentRepository.GetCommentById(command.CommentId).Value;

        if (comment is null)
        {
            Error error = Errors.Comments.CommentNotFound;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        var reply = comment.Replies.FirstOrDefault(v => v.Id == command.ReplyId);

        if (reply is null)
        {
            Error error = Errors.CommentReplies.ReplyNotFound;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        if (reply.UserId != command.UserId)
        {
            Error error = Errors.CommentReplies.ReplyNotOwnerByUser;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        _commentRepository.DeleteCommentReplyById(command.CommentId, command.ReplyId, command.UserId);

        DeleteCommentReplyResult result = new("Reply successfully delete.");

        Log.Information(
            "{@DeleteReplyOnCommentResult}",
            result);

        return result;
    }
}