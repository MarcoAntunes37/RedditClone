namespace RedditClone.Application.PostVotes.Commands.CreateVote;

using ErrorOr;
using MediatR;
using Serilog;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.Post.Results.CreatePostVoteResult;

public class CreatePostVoteCommandHandler
    : IRequestHandler<CreatePostVoteCommand, ErrorOr<CreatePostVoteResult>>
{
    private readonly IPostRepository _postRepository;

    public CreatePostVoteCommandHandler(
        IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<ErrorOr<CreatePostVoteResult>> Handle(
        CreatePostVoteCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@CreateVoteCommand}",
            "Trying to vote on Post: {@PostId}",
            command,
            command.PostId);

        if (_postRepository.GetPostById(command.PostId).Value is null)
        {
            Error error = Errors.Posts.PostNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        if(!_postRepository.UserExists(command.UserId))
        {
            Error error = Errors.User.UserNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        var success = _postRepository.AddPostVote(command.PostId, command.UserId, command.IsVoted);

        if (!success.Value)
        {
            Error error = Errors.PostVotes.UserAlreadyVoted;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        CreatePostVoteResult result = new("Vote on post successfully");

        Log.Information(
            "{@CreateVoteResult}, {@PostId}",
            command,
            command.PostId);

        return result;
    }
}