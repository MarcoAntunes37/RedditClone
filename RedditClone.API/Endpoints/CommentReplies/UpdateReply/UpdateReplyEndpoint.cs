
using ErrorOr;
using MediatR;
using RedditClone.API.Extension;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.CommentReplies.Commands.UpdateCommentReply;
using RedditClone.Application.CommentReplies.Results.UpdateCommentReplyResults;

namespace RedditClone.API.Endpoints.CommentReplies.UpdateReply;

public class UpdateReplyEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/comments/{commentId}/replies/{replyId}/update", async (
            Guid commentId,
            Guid replyId,
            UpdateReplyRequest req,
            ISender mediator) =>
        {
            var command = new UpdateCommentReplyCommand(
                new CommentId(commentId),
                new ReplyId(replyId),
                new UserId(req.UserId),
                req.Content);

            ErrorOr<UpdateCommentReplyResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => ProblemExtensions.CreateProblemDetails(errors));

        }).MapToApiVersion(1)
        .WithTags(Tags.CommentReplies)
        .RequireAuthorization();
    }
}