namespace RedditClone.Application.ReplyVotes.Commands.DeleteReplyVote;

using ErrorOr;
using MediatR;
using Serilog;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Persistence;
using RedditClone.Application.ReplyVotes.Results.DeleteReplyVoteResult;

public class DeleteReplyVoteCommandHandler(
    ICommentRepository commentRepository)
    : IRequestHandler<DeleteReplyVoteCommand, ErrorOr<DeleteReplyVoteResult>>
{
    private readonly ICommentRepository _commentRepository = commentRepository;

    public async Task<ErrorOr<DeleteReplyVoteResult>> Handle(
        DeleteReplyVoteCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@DeleteVoteOnReplyCommand}",
            "Trying to delete Vote: {@VoteId} on Reply: {@ReplyId}",
            command,
            command.VoteId,
            command.ReplyId);

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

        if (!_commentRepository.UserExists(command.UserId))
        {
            Error error = Errors.User.UserNotFound;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }
        var reply = comment.Replies.FirstOrDefault(r => r.Id == command.ReplyId);

        if (reply is null)
        {
            Error error = Errors.CommentReplies.ReplyNotFound;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        var vote = reply.Votes.FirstOrDefault(v => v.Id == command.VoteId);

        if (vote is null)
        {
            Error error = Errors.ReplyVotes.VoteNotFound;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        _commentRepository.DeleteReplyVoteById(command.CommentId, command.ReplyId, command.VoteId, command.UserId);

        DeleteReplyVoteResult result = new("Vote deleted successfully.");

        Log.Information(
            "{@DeleteVoteOnReplyResult}",
            result);

        return result;
    }
}