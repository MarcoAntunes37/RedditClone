namespace RedditClone.Application.Post.Commands.DeletePost;

using ErrorOr;
using MediatR;
using Serilog;
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

        _postRepository.DeletePostById(command.PostId, command.UserId);

        DeletePostResult result = new("Post deleted successfully");

        Log.Information(
            "{@DeletePostResult}",
            result,
            command.PostId);

        return result;
    }
}