namespace RedditClone.API.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedditClone.API.Mappers;
using RedditClone.Application.Comment.Results.CreateCommentResult;
using RedditClone.Application.Community.Results.DeleteCommentResult;
using RedditClone.Application.Community.Results.UpdateCommentResult;
using RedditClone.Contracts.Comment;
using RedditClone.Contracts.Comment.DeleteComment;
using RedditClone.Contracts.Comment.UpdateComment;

[Route("comments")]
public class CommentController : ApiController
{
    private readonly ISender _sender;

    public CommentController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("{postId}/new-comment")]
    public async Task<IActionResult> CreateComment(
        [FromBody] CreateCommentRequest request,
        [FromRoute] Guid postId)
    {
        var command = CommentMappers.MapCreateCommentRequest(request, postId);

        CreateCommentResult result = await _sender.Send(command);

        return Ok(CommentMappers.MapCreateCommentResponse(result));
    }

    [HttpPut("update-comment/{commentId}")]
    public async Task<IActionResult> UpdateComment(
        [FromBody] UpdateCommentRequest request,
        [FromRoute] Guid commentId)
    {
        var command = CommentMappers.MapUpdateCommentRequest(request, commentId);

        UpdateCommentResult result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpDelete("delete-comment/{commentId}")]
    public async Task<IActionResult> DeleteComment(
        [FromBody] DeleteCommentRequest request,
        [FromRoute] Guid commentId)
    {
        var command = CommentMappers.MapDeleteCommentRequest(commentId, request);

        DeleteCommentResult result = await _sender.Send(command);

        return Ok(result);
    }
}