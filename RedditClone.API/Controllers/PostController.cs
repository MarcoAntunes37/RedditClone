namespace RedditClone.API.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedditClone.Contracts.Post;
using RedditClone.Application.Post.Results.CreatePostResult;
using RedditClone.API.Mappers;
using RedditClone.Contracts.Community.UpdatePost;
using RedditClone.Application.Post.Results.UpdatePostResult;
using RedditClone.Contracts.Community.DeletePost;
using RedditClone.Application.Post.Results.DeletePostResult;

[Route("posts/")]
public class PostController : ApiController
{
    private readonly ISender _sender;

    public PostController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("new-post")]
    public async Task<IActionResult> CreatePost(
        [FromBody] CreatePostRequest request)
    {
        var command = PostMappers.MapCreatePostRequest(request);

        CreatePostResult result = await _sender.Send(command);

        return Ok(PostMappers.MapCreatePostResponse(result));
    }

    [HttpPut("update-post/{postId}")]
    public async Task<IActionResult> UpdatePost(
        [FromRoute] Guid postId,
        [FromBody] UpdatePostRequest request)
    {
        var command = PostMappers.MapUpdatePostRequest(postId, request);

        UpdatePostResult result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpDelete("delete-post/{postId}")]
    public async Task<IActionResult> DeletePost(
        [FromRoute] Guid postId,
        [FromBody] DeletePostRequest request)
    {
        var command = PostMappers.MapDeletePostRequest(postId, request);

        DeletePostResult result = await _sender.Send(command);

        return Ok(result);
    }
}