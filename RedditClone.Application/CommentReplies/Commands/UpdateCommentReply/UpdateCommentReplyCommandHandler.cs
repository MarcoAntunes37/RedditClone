namespace RedditClone.Application.CommentReplies.Commands.UpdateCommentReply;

using ErrorOr;
using MediatR;
using Serilog;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Persistence;
using RedditClone.Application.CommentReplies.Results.UpdateCommentReplyResults;

public class UpdateCommentReplyCommandHandler(
    ICommentRepository commentRepository)
: IRequestHandler<UpdateCommentReplyCommand, ErrorOr<UpdateCommentReplyResult>>
{
    private readonly ICommentRepository _commentRepository = commentRepository;

    public async Task<ErrorOr<UpdateCommentReplyResult>> Handle(
        UpdateCommentReplyCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@UpdateReplyOnCommentCommand}",
            "Trying to update Reply: {@ReplyId} on Comment: {@CommentId}",
            command,
            command.ReplyId,
            command.CommentId);

        if(_commentRepository.GetCommentById(command.CommentId).Value is null)
        {
            Error error = Errors.Comments.CommentNotFound;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        _commentRepository.UpdateCommentReplyById(command.CommentId, command.ReplyId, command.UserId, command.Content);

        UpdateCommentReplyResult result = new("Comment reply updated successfully");

        Log.Information(
            "{@UpdateReplyOnCommentResult}",
            result);

        return result;
    }
}