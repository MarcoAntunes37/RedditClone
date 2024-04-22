namespace RedditClone.Application.Post.Commands.UpdatePost;

using MediatR;
using Serilog;
using ErrorOr;
using RedditClone.Domain.PostAggregate;
using RedditClone.Application.Post.Results.UpdatePostResult;
using RedditClone.Application.Common.Interfaces.Persistence;

public class UpdatePostCommandHandler(
    IPostRepository postRepository)
: IRequestHandler<UpdatePostCommand, ErrorOr<UpdatePostResult>>
{
    private readonly IPostRepository _postRepository = postRepository;
    public async Task<ErrorOr<UpdatePostResult>> Handle(
        UpdatePostCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@Command}",
            "Trying to update Post: {@PostId}",
            command,
            command.PostId);

        Post? post = _postRepository.UpdatePostById(command.PostId, command.UserId, command.Title, command.Content).Value;

        UpdatePostResult result = new("Post updated successfully",post);

        Log.Information(
            "{@UpdatePostResult}");

        return result;
    }
}