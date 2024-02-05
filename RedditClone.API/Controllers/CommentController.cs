using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedditClone.Application.Comment.Commands.CreateCommentCommand;
using RedditClone.Application.Comment.Results.CreateCommentResult;
using RedditClone.Contracts.Comment;
using RedditClone.Contracts.Community;

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
        [FromBody] CreateCommentRequest request)
    {
        var command = MapCreateCommentCommand(request);

        CreateCommentResult result = await _sender.Send(command);

        return Ok(MapCreateCommentResponse(result));
    }

    private static CreateCommentCommand MapCreateCommentCommand(
        CreateCommentRequest request)
    {
        return new CreateCommentCommand(
            request.Content,
            DateTime.Now,
            DateTime.Now
        );
    }

    private static CreateCommentResponse MapCreateCommentResponse(
        CreateCommentResult result)
    {
        var comment = result.Comment;
        return new CreateCommentResponse(
            comment.Id.Value.ToString(),
            comment.Content,
            comment.CreatedAt,
            comment.UpdatedAt
        );
    }
}