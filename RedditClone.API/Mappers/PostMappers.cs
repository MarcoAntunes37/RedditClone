using RedditClone.Application.Post.Commands.CreatePost;
using RedditClone.Application.Post.Commands.DeletePost;
using RedditClone.Application.Post.Commands.UpdatePost;
using RedditClone.Application.Post.Results.CreatePostResult;
using RedditClone.Contracts.Community.DeletePost;
using RedditClone.Contracts.Community.UpdatePost;
using RedditClone.Contracts.Post;
using RedditClone.Contracts.Post.CreatePost.Models;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.PostAggregate.Entities;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

namespace RedditClone.API.Mappers;

public class PostMappers{
    public static CreatePostCommand MapCreatePostRequest(
        CreatePostRequest request)
    {
        List<Votes> votes = new();
        return new CreatePostCommand(
            new CommunityId(request.CommunityId),
            new UserId(request.UserId),
            request.Title,
            request.Content,
            DateTime.UtcNow,
            DateTime.UtcNow,
            votes
        );
    }

    public static CreatePostResponse MapCreatePostResponse(
        CreatePostResult result)
    {
        var post = result.Post;
        List<CreatePostVotes> votes = new();
        return new CreatePostResponse(
            post.Id.Value.ToString(),
            post.CommunityId.Value.ToString(),
            post.UserId.Value.ToString(),
            post.Title,
            post.Content,
            post.CreatedAt,
            post.UpdatedAt,
            votes
        );
    }

    public static UpdatePostCommand MapUpdatePostRequest(
        Guid postId,
        UpdatePostRequest request)
    {
        return new UpdatePostCommand(
            new PostId(postId),
            new UserId(request.UserId),
            request.Title,
            request.Content
        );
    }

    public static DeletePostCommand MapDeletePostRequest(
        Guid postId,
        DeletePostRequest request)
    {
        return new DeletePostCommand(
            new PostId(postId),
            new UserId(request.UserId)
        );
    }
}