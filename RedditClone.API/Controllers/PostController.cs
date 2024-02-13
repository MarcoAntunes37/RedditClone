namespace RedditClone.API.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedditClone.Application.Post.Commands.CreatePost;
using RedditClone.Contracts.Post;
using RedditClone.Application.Post.Results;
using RedditClone.Domain.PostAggregate.Entities;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Contracts.Post.CreatePost.Models;

[Route("{userId}/communities/{communityId}/posts/new-post")]
public class PostController : ApiController
{
    private readonly ISender _sender;

    public PostController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost(
        [FromBody] CreatePostRequest request,
        [FromRoute] Guid communityId,
        [FromRoute] Guid userId)
    {
        var command = MapCreatePostCommand(request, userId, communityId);

        CreatePostResult result = await _sender.Send(command);

        return Ok(MapCreatePostResponse(result));
    }

    private static CreatePostCommand MapCreatePostCommand(
        CreatePostRequest request,
        Guid communityId,
        Guid userId)
    {
        List<Votes> votes = new();
        return new CreatePostCommand(
            CommunityId.Create(communityId),
            UserId.Create(userId),
            request.Title,
            request.Content,
            DateTime.UtcNow,
            DateTime.UtcNow,
            votes
        );
    }

    private static CreatePostResponse MapCreatePostResponse(
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
}