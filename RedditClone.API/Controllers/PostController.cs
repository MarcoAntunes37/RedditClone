namespace RedditClone.API.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedditClone.Application.Post.Results.CreatePostResult;
using RedditClone.API.Mappers;
using RedditClone.Application.Post.Results.UpdatePostResult;
using RedditClone.Application.Post.Results.DeletePostResult;
using RedditClone.Contracts.Post.VoteOnPost;
using RedditClone.Application.Post.Results.VoteOnPostResult;
using RedditClone.Contracts.Post.UpdateVoteOnPost;
using RedditClone.Application.Post.Results.UpdateVoteOnPostResult;
using RedditClone.Contracts.Post.DeletePost;
using RedditClone.Contracts.Post.UpdatePost;
using RedditClone.Contracts.Post.CreatePost;
using RedditClone.Contracts.Post.DeleteVoteOnPost;
using RedditClone.Application.Post.Results.DeleteVoteOnPostResult;

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

    [HttpPut("vote-on-post/{postId}")]
    public async Task<IActionResult> VoteOnPost(
        [FromRoute] Guid postId,
        [FromBody] VoteOnPostRequest request)
    {
        var command = PostMappers.MapVoteOnPostRequest(postId, request);

        VoteOnPostResult result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpPut("vote-on-post/{postId}/update/{voteId}")]
    public async Task<IActionResult> UpdateVoteOnPost(
        [FromRoute] Guid postId,
        [FromRoute] Guid voteId,
        [FromBody] UpdateVoteOnPostRequest request)
    {
        var command = PostMappers.MapUpdateVoteOnPostRequest(postId, voteId, request);

        UpdateVoteOnPostResult result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpDelete("vote-on-post/{postId}/delete/{voteId}")]
    public async Task<IActionResult> DeleteVoteOnPost(
        [FromRoute] Guid postId,
        [FromRoute] Guid voteId,
        [FromBody] DeleteVoteOnPostRequest request)
    {
        var command = PostMappers.MapDeleteVoteOnPostRequest(postId, voteId, request);

        DeleteVoteOnPostResult result = await _sender.Send(command);

        return Ok(result);
    }
}