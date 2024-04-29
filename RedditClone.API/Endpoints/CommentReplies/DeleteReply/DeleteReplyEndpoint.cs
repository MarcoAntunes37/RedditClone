namespace RedditClone.API.Endpoints.CommentReplies.DeleteReply;

using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedditClone.API.Extension;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.CommentReplies.Commands.DeleteCommentReply;
using RedditClone.Application.CommentReplies.Results.DeleteCommentReplyResults;

public class DeleteReplyEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/comments/{commentId}/replies/{replyId}/delete", async (
            Guid commentId,
            Guid replyId,
            [FromBody] DeleteReplyRequest req,
            ISender mediator) =>
        {
            var command = new DeleteCommentReplyCommand(
                new CommentId(commentId),
                new ReplyId(replyId),
                new UserId(req.UserId));

            ErrorOr<DeleteCommentReplyResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => ProblemExtensions.CreateProblemDetails(errors));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.CommentReplies)
        .RequireAuthorization();
    }
}