using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedditClone.Application.Post.Commands.CreatePost;
using RedditClone.Contracts.Post;
using RedditClone.Application.Post.Results;

namespace RedditClone.API.Controllers;

[Route("communities/posts/new-post")]
public class PostController : ApiController
{
    private readonly ISender _sender;

    public PostController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost(
        [FromBody] CreatePostRequest request)
    {
        var command = MapCreatePostCommand(request);

        CreatePostResult result = await _sender.Send(command);

        return Ok(MapCreatePostResponse(result));
    }

    private static CreatePostCommand MapCreatePostCommand(
        CreatePostRequest request)
    {
        return new CreatePostCommand(
            request.Title,
            request.Content,
            DateTime.Now,
            DateTime.Now
        );
    }

    private static CreatePostResponse MapCreatePostResponse(
        CreatePostResult result)
    {
        var post = result.Post;
        return new CreatePostResponse(
            post.Id.Value.ToString(),
            post.Title,
            post.Content,
            post.CreatedAt,
            post.UpdatedAt
        );
    }
}