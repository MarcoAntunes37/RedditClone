using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedditClone.Application.Comment.Commands.CreateCommentCommand;
using RedditClone.Contracts.Comment;
using RedditClone.Contracts.Comment.CreateComment.Models;
using RedditClone.Contracts.Community;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.CommentAggregate.ValueObjects;

namespace RedditClone.API.Controllers;

[Route("communities/posts/{postId}/{userId}/new-comment")]
public class CommentController : ApiController
{
    private readonly ISender _sender;

    public CommentController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCommunity(
        [FromBody]CreateCommentRequest request,
        [FromRoute]string userId,
        [FromRoute]string postId)
    {
        var command = MapCreateCommentCommand(request, userId, postId);

        var createCommentResult = await _sender.Send(command);

        return createCommentResult.Match(
            comment => Ok(MapCreateCommentResponse(comment)),
            errors => Problem(errors)
        );
    }

    private static CreateCommentCommand MapCreateCommentCommand(
        CreateCommentRequest request,
        string userId,
        string postId)
    {
        List<Replies> replies = new();
        List<Upvotes> upvotes = new();
        List<Downvotes> downvotes = new();
        return new CreateCommentCommand(
            UserId.Create(userId),
            PostId.Create(postId),
            request.Content,
            DateTime.Now,
            DateTime.Now,
            replies,
            upvotes,
            downvotes
        );
    }

    private static CreateCommentResponse MapCreateCommentResponse(
        CommentAggregate comment)
    {
        return new CreateCommentResponse(
            comment.Id.Value.ToString(),
            comment.UserId.Value.ToString(),
            comment.PostId.Value.ToString(),
            comment.Content,
            comment.CreatedAt,
            comment.UpdatedAt,
            new List<CreateCommentReplies>(),
            new List<CreateCommentUpvotes>(),
            new List<CreateCommentDownvotes>()
        );
    }
}