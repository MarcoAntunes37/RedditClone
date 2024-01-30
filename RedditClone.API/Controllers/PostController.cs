using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedditClone.Application.Post.Commands.CreatePost;
using RedditClone.Domain.PostAggregate.Entities;
using RedditClone.Domain.PostAggregate;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Contracts.Post;
using RedditClone.Contracts.Post.CreatePost.Models;

namespace RedditClone.API.Controllers;

[Route("communities/{communityId}/posts/{userId}/new-post")]
public class PostController : ApiController
{
    private readonly ISender _sender;

    public PostController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost(
        [FromBody]CreatePostRequest request,
        [FromRoute]string communityId,
        [FromRoute]string userId)
    {
        var command = MapCreatePostCommand(request, communityId, userId);

        var createPostResult = await _sender.Send(command);

        return createPostResult.Match(
            post => Ok(MapCreatePostResponse(post)),
            errors => Problem(errors)
        );
    }

    private static CreatePostCommand MapCreatePostCommand(
        CreatePostRequest request,
        string communityId,
        string userId
        ){
        List<Upvotes> upvotes = new();
        List<Downvotes> downvotes = new();

        return new CreatePostCommand(
            request.Title,
            request.Content,
            UserId.Create(userId),
            CommunityId.Create(communityId),
            DateTime.Now,
            DateTime.Now,
            upvotes,
            downvotes
        );
    }

    private static CreatePostResponse MapCreatePostResponse(
        PostAggregate post)
    {
        return new CreatePostResponse(
            post.Id.Value.ToString(),
            post.Title,
            post.Content,
            post.UserId.Value.ToString(),
            post.CommunityId.Value.ToString(),
            post.CreatedAt,
            post.UpdatedAt,
            new List<CreatePostUpvotes>(),
            new List<CreatePostDownvotes>()
        );
    }
}