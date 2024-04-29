namespace RedditClone.Application.Post.Commands.UpdatePost;

using MediatR;
using Serilog;
using ErrorOr;
using RedditClone.Domain.PostAggregate;
using RedditClone.Domain.Common.Errors;
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


        var post = _postRepository.GetPostById(command.PostId).Value;

        if (post is null)
        {
            Error error = Errors.Posts.PostNotFound;

            Log.Error(
                "{@Message}, {@Error}",
                "Post not found",
                error);

            return error;
        }

        if(post.UserId != command.UserId)
        {
            Error error = Errors.Posts.PostNotOwnedByUser;

            Log.Error(
                "{@Message}, {@Error}",
                "Post not owned by user",
                error);

            return error;
        }

        Post updatedPost = _postRepository.UpdatePostById(command.PostId, command.Title, command.Content);

        UpdatePostResult result = new("Post updated successfully", updatedPost);

        Log.Information(
            "{@UpdatePostResult}");

        return result;
    }
}