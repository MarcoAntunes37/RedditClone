namespace RedditClone.Application.PostVotes.Commands.DeleteVote;

using ErrorOr;
using MediatR;
using Serilog;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.Post.Results.DeletePostVoteResult;

public class DeletePostVoteCommandHandler(
    IPostRepository postRepository)
        : IRequestHandler<DeletePostVoteCommand, ErrorOr<DeletePostVoteResult>>
{
    private readonly IPostRepository _postRepository = postRepository;

    public async Task<ErrorOr<DeletePostVoteResult>> Handle(
        DeletePostVoteCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@DeleteVoteCommand}",
            "Trying to delete Vote: {@VoteId} in Post: {@PostId}",
            command,
            command.VoteId,
            command.PostId);

        if(_postRepository.GetPostById(command.PostId).Value == null)
        {
            Error error = Errors.Posts.PostNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        _postRepository.DeletePostVoteById(command.PostId, command.VoteId, command.UserId);

        DeletePostVoteResult result = new("Vote on post deleted successfully");

        Log.Information(
            "{@DeleteVoteResult}, {@VoteId}",
            result,
            command.VoteId);

        return result;
    }
}