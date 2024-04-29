namespace RedditClone.Application.ReplyVotes.Commands.UpdateReplyVote;

using MediatR;
using ErrorOr;
using Serilog;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Persistence;
using RedditClone.Application.ReplyVotes.Results.UpdateReplyVoteResult;

public class UpdateReplyVoteCommandHandler(
    ICommentRepository commentRepository)
        : IRequestHandler<UpdateReplyVoteCommand, ErrorOr<UpdateReplyVoteResult>>
{
    private readonly ICommentRepository _commentRepository = commentRepository;

    public async Task<ErrorOr<UpdateReplyVoteResult>> Handle(
        UpdateReplyVoteCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information("{@Message}, {@UpdateReplyVoteCommand}",
            "Trying to update reply vote {@VoteId}",
            command,
            command.VoteId);

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

        if (!_commentRepository.UserExists(command.UserId))
        {
            Error error = Errors.User.UserNotFound;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        if(reply.UserId != command.UserId)
        {
            Error error = Errors.ReplyVotes.UserNotVoteOwner;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        _commentRepository.UpdateReplyVoteById(command.CommentId, command.ReplyId, command.VoteId, command.UserId, command.IsVoted);

        UpdateReplyVoteResult result = new("Comment reply vote updated successfully");

        Log.Information(
            "{@UpdateReplyVoteResult}",
            result);

        return result;
    }
}