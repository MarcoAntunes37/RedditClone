namespace RedditClone.Application.PostVotes.Commands.UpdateVote;

using ErrorOr;
using MediatR;
using Serilog;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.PostVotes.Results.UpdatePostVoteResult;

public class UpdatePostVoteCommandHandler(
    IPostRepository postRepository)
: IRequestHandler<UpdatePostVoteCommand, ErrorOr<UpdatePostVoteResult>>
{
    private readonly IPostRepository _postRepository = postRepository;

    public async Task<ErrorOr<UpdatePostVoteResult>> Handle(
        UpdatePostVoteCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@UpdateVoteCommand}",
            "Trying to update Vote: {@VoteId} on Post: {@PostId}",
            command.VoteId,
            command.PostId);

        if (_postRepository.GetPostById(command.PostId).Value == null)
        {
            Error error = Errors.Posts.PostNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        var success = _postRepository.UpdatePostVoteById(command.PostId, command.VoteId, command.UserId, command.IsVoted);

        if (!success.Value)
        {
            Error error = Errors.PostVotes.UserNotVoteOwner;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        UpdatePostVoteResult result = new("Vote on post updated successfully");

        Log.Information(
            "{@UpdateVoteResult}, {@VoteId}",
            result,
            command.VoteId);

        return result;
    }
}