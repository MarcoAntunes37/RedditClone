namespace RedditClone.Application.Post.Commands.DeletePost;

using ErrorOr;
using MediatR;
using Serilog;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.Post.Results.DeletePostResult;

public class DeletePostCommandHandler(
    IPostRepository postRepository)
        : IRequestHandler<DeletePostCommand, ErrorOr<DeletePostResult>>
{
    private readonly IPostRepository _postRepository = postRepository;

    public async Task<ErrorOr<DeletePostResult>> Handle(
        DeletePostCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@CreatePostCommand}",
            "Trying to delete post: {@PostId}",
            command.PostId);

        if(!_postRepository.UserExists(command.UserId))
        {
            Error error = Errors.User.UserNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        var success = _postRepository.DeletePostById(command.PostId, command.UserId);

        if (!success.Value)
        {
            Error error = Errors.Posts.PostNotOwnedByUser;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        DeletePostResult result = new("Post deleted successfully");

        Log.Information(
            "{@DeletePostResult}",
            result,
            command.PostId);

        return result;
    }
}