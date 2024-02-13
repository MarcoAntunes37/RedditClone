namespace RedditClone.API.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedditClone.Application.Comment.Commands.CreateCommentCommand;
using RedditClone.Application.Comment.Results.CreateCommentResult;
using RedditClone.Contracts.Comment;
using RedditClone.Contracts.Comment.CreateComment.Models;
using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.CommentAggregate.ValueObjects;

[Route("{userId}/communities/posts/{postId}/new-comment")]
public class CommentController : ApiController
{
    private readonly ISender _sender;

    public CommentController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment(
        [FromBody] CreateCommentRequest request,
        [FromRoute] Guid userId,
        [FromRoute] Guid postId)
    {
        var command = MapCreateCommentCommand(request, userId, postId);

        CreateCommentResult result = await _sender.Send(command);

        return Ok(MapCreateCommentResponse(result));
    }

    private static CreateCommentCommand MapCreateCommentCommand(
        CreateCommentRequest request,
        Guid userId,
        Guid postId)
    {
        List<Votes> votes = new();
        List<Replies> replies = new();
        return new CreateCommentCommand(
            UserId.Create(userId),
            PostId.Create(postId),
            request.Content,
            DateTime.UtcNow,
            DateTime.UtcNow,
            votes,
            replies
        );
    }

    private static CreateCommentResponse MapCreateCommentResponse(
        CreateCommentResult result)
    {
        var comment = result.Comment;
        List<CreateCommentVotes> votes = new();
        List<CreateCommentReplies> replies = new();
        return new CreateCommentResponse(
            comment.Id.Value.ToString(),
            comment.UserId.Value.ToString(),
            comment.PostId.Value.ToString(),
            comment.Content,
            comment.CreatedAt,
            comment.UpdatedAt,
            votes,
            replies
        );
    }
}