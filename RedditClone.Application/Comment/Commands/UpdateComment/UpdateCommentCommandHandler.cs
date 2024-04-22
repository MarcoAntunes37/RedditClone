namespace RedditClone.Application.Comment.Commands.UpdateComment;

using ErrorOr;
using MediatR;
using Serilog;
using RedditClone.Domain.Common.Errors;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Application.Persistence;
using RedditClone.Application.Comment.Results.UpdateCommentResult;

public class UpdateCommentCommandHandler(
    ICommentRepository commentRepository)
    : IRequestHandler<UpdateCommentCommand, ErrorOr<UpdateCommentResult>>
{
    private readonly ICommentRepository _commentRepository = commentRepository;

    public async Task<ErrorOr<UpdateCommentResult>> Handle(
        UpdateCommentCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@UpdateCommentCommand}",
            "Trying to update Comment: {@CommentId}",
            command,
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

        Comment comment = _commentRepository.UpdateCommentById(command.CommentId, command.UserId, command.Content).Value;

        UpdateCommentResult result = new("Comment successfully updated.", comment);

        Log.Information(
            "{@UpdateCommentResult}",
            result);

        return result;
    }
}