namespace RedditClone.Application.CommentVotes.Commands.UpdateCommentVote;

using ErrorOr;
using MediatR;
using Serilog;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Persistence;
using RedditClone.Application.CommentVotes.Results.UpdateCommentVoteResult;

public class UpdateCommentVoteCommandHandler(
    ICommentRepository commentRepository)
: IRequestHandler<UpdateCommentVoteCommand, ErrorOr<UpdateCommentVoteResult>>
{
    private readonly ICommentRepository _commentRepository = commentRepository;

    public async Task<ErrorOr<UpdateCommentVoteResult>> Handle(
        UpdateCommentVoteCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@UpdateVoteOnCommentCommand}",
            "Trying to update Vote: {@VoteId} on Comment: {CommentId}",
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

        var vote = comment.Votes.FirstOrDefault(v => v.Id == command.VoteId)!;

        if (vote is null){

            Error error = Errors.CommentVotes.VoteNotFound;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        if (vote.UserId != command.UserId)
        {
            Error error = Errors.CommentVotes.UserNotVoteOwner;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        _commentRepository.UpdateCommentVoteById(command.CommentId, command.VoteId, command.UserId, command.IsVoted);

        UpdateCommentVoteResult result = new("Comment successfully updated.");

        Log.Information(
            "{@UpdateVoteOnCommentResult}",
            result);

        return result;
    }
}