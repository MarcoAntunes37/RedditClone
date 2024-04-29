namespace RedditClone.API.Endpoints.Comment.UpdateComment;

using ErrorOr;
using MediatR;
using RedditClone.API.Extension;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.Comment.Commands.UpdateComment;
using RedditClone.Application.Comment.Results.UpdateCommentResult;

public class UpdateCommentEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/comments/{commentId}/update", async (
            Guid commentId,
            UpdateCommentRequest req,
            ISender mediator
        ) =>
        {
            var command = new UpdateCommentCommand(
                new CommentId(commentId),
                new UserId(req.UserId),
                req.Content);

            ErrorOr<UpdateCommentResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => ProblemExtensions.CreateProblemDetails(errors));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.Comments)
        .RequireAuthorization();
    }
}