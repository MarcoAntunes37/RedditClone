namespace RedditClone.Application.CommentVotes.Commands.DeleteCommentVote;

using ErrorOr;
using MediatR;
using Serilog;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Persistence;
using RedditClone.Application.CommentVotes.Results.DeleteCommentVoteResult;

public class DeleteCommentVoteCommandHandler(
    ICommentRepository commentRepository)
: IRequestHandler<DeleteCommentVoteCommand, ErrorOr<DeleteCommentVoteResult>>
{
    private readonly ICommentRepository _commentRepository = commentRepository;

    public async Task<ErrorOr<DeleteCommentVoteResult>> Handle(
        DeleteCommentVoteCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@DeleteVoteOnCommentCommand}",
            "Trying to delete Vote: {@VoteId} on Comment: {@CommentId}",
            command,
            command.VoteId,
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

        _commentRepository.DeleteCommentVoteById(command.CommentId, command.VoteId, command.UserId);

        DeleteCommentVoteResult result = new("Comment successfully deleted.");

        Log.Information(
            "{@DeleteVoteOnCommentResult}",
            result);

        return result;
    }
}