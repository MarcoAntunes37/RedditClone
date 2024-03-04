namespace RedditClone.API.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedditClone.API.Mappers;
using RedditClone.Application.Comment.Results.CreateCommentResult;
using RedditClone.Application.Comment.Results.ReplyOnCommentResult;
using RedditClone.Application.Comment.Results.UpdateReplyOnCommentResult;
using RedditClone.Application.Comment.Results.UpdateVoteOnReplyResult;
using RedditClone.Application.Comment.Results.VoteOnCommentResult;
using RedditClone.Application.Comment.Results.VoteOnReplyResult;
using RedditClone.Application.Community.Results.DeleteCommentResult;
using RedditClone.Application.Community.Results.DeleteReplyOnCommentResult;
using RedditClone.Application.Community.Results.DeleteVoteOnCommentResult;
using RedditClone.Application.Community.Results.DeleteVoteOnReplyResult;
using RedditClone.Application.Community.Results.UpdateCommentResult;
using RedditClone.Application.Community.Results.UpdateVoteOnCommentResult;
using RedditClone.Contracts.Comment.CreateComment;
using RedditClone.Contracts.Comment.DeleteComment;
using RedditClone.Contracts.Comment.DeleteReplyOnComment;
using RedditClone.Contracts.Comment.DeleteVoteOnComment;
using RedditClone.Contracts.Comment.DeleteVoteOnReply;
using RedditClone.Contracts.Comment.ReplyOnComment;
using RedditClone.Contracts.Comment.UpdateComment;
using RedditClone.Contracts.Comment.UpdateReplyOnComment;
using RedditClone.Contracts.Comment.UpdateVoteOnComment;
using RedditClone.Contracts.Comment.UpdateVoteOnReply;
using RedditClone.Contracts.Comment.VoteOnComment;
using RedditClone.Contracts.Comment.VoteOnReply;

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

    [HttpPut("vote-on-comment/{commentId}")]
    public async Task<IActionResult> VoteOnComment(
        [FromRoute] Guid commentId,
        [FromBody] VoteOnCommentRequest request)
    {
        var command = CommentMappers.MapVoteOnCommentRequest(commentId, request);

        VoteOnCommentResult result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpPut("vote-on-comment/{commentId}/update/{voteId}")]
    public async Task<IActionResult> UpdateVoteOnComment(
        [FromBody] UpdateVoteOnCommentRequest request,
        [FromRoute] Guid commentId,
        [FromRoute] Guid voteId)
    {
        var command = CommentMappers.MapUpdateVoteOnCommentRequest(request, commentId, voteId);

        UpdateVoteOnCommentResult result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpDelete("vote-on-comment/{commentId}/delete/{voteId}")]
    public async Task<IActionResult> DeleteVoteOnComment(
        [FromBody] DeleteVoteOnCommentRequest request,
        [FromRoute] Guid commentId,
        [FromRoute] Guid voteId)
    {
        var command = CommentMappers.MapDeleteVoteOnCommentRequest(commentId, voteId, request);

        DeleteVoteOnCommentResult result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpPut("reply-on-comment/{commentId}")]
    public async Task<IActionResult> ReplyOnComment(
        [FromRoute] Guid commentId,
        [FromBody] ReplyOnCommentRequest request)
    {
        var command = CommentMappers.MapReplyOnCommentRequest(commentId, request);

        ReplyOnCommentResult result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpPut("reply-on-comment/{commentId}/update/{replyId}")]
    public async Task<IActionResult> UpdateReplyOnComment(
        [FromBody] UpdateReplyOnCommentRequest request,
        [FromRoute] Guid commentId,
        [FromRoute] Guid replyId)
    {
        var command = CommentMappers.MapUpdateReplyOnCommentRequest(request, commentId, replyId);

        UpdateReplyOnCommentResult result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpDelete("reply-on-comment/{commentId}/delete/{replyId}")]
    public async Task<IActionResult> DeleteReplyOnComment(
        [FromBody] DeleteReplyOnCommentRequest request,
        [FromRoute] Guid commentId,
        [FromRoute] Guid replyId)
    {
        var command = CommentMappers.MapDeleteReplyOnCommentRequest(request, commentId, replyId);

        DeleteReplyOnCommentResult result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpPut("vote-on-reply/{commentId}/reply/{replyId}")]
    public async Task<IActionResult> VoteOnReply(
        [FromRoute] Guid commentId,
        [FromRoute] Guid replyId,
        [FromBody] VoteOnReplyRequest request)
    {
        var command = CommentMappers.MapVoteOnReplyRequest(request, commentId, replyId);

        VoteOnReplyResult result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpPut("reply-on-comment/{commentId}/update/{replyId}/vote/{voteId}")]
    public async Task<IActionResult> UpdateVoteOnReply(
        [FromBody] UpdateVoteOnReplyRequest request,
        [FromRoute] Guid commentId,
        [FromRoute] Guid replyId,
        [FromRoute] Guid voteId)
    {
        var command = CommentMappers.MapUpdateVoteOnReplyRequest(request, replyId, voteId, commentId);

        UpdateVoteOnReplyResult result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpDelete("reply-on-comment/{commentId}/delete/{replyId}/vote/{voteId}")]
    public async Task<IActionResult> DeleteVoteOnReply(
        [FromBody] DeleteVoteOnReplyRequest request,
        [FromRoute] Guid commentId,
        [FromRoute] Guid replyId,
        [FromRoute] Guid voteId)
    {
        var command = CommentMappers.MapDeleteVoteOnReplyRequest(request, commentId, replyId, voteId);

        DeleteVoteOnReplyResult result = await _sender.Send(command);

        return Ok(result);
    }
}