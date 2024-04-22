namespace RedditClone.Application.Comment.Commands.DeleteComment;

using ErrorOr;
using MediatR;
using Serilog;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Persistence;
using RedditClone.Application.Comment.Results.DeleteCommentResult;

public class DeleteCommentCommandHandler(
    ICommentRepository commentRepository)
    : IRequestHandler<DeleteCommentCommand, ErrorOr<DeleteCommentResult>>
{
    private readonly ICommentRepository _commentRepository = commentRepository;

    public async Task<ErrorOr<DeleteCommentResult>> Handle(
        DeleteCommentCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@DeleteCommentCommand}",
            "Trying to delete comment {CommentId}",
            command,
            command.CommentId);

        var comment = _commentRepository.GetCommentById(command.CommentId).Value;

        if(comment is null)
        {
            Error error = Errors.Comments.CommentNotFound;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        _commentRepository.DeleteCommentById(command.CommentId, command.UserId);

        DeleteCommentResult result = new("Comment successfully deleted.");

        Log.Information(
           "{@DeleteCommentResult}, {@CommentId}",
           result,
           command.CommentId);

        return result;
    }
}