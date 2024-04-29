namespace RedditClone.API.Endpoints.Comment.DeleteComment;

using ErrorOr;
using MediatR;
using RedditClone.API.Extension;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.Comment.Commands.DeleteComment;
using RedditClone.Application.Comment.Results.DeleteCommentResult;

public class DeleteCommentEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/comments/{commentId}/delete/{currentUserId}", async (
            Guid commentId,
            Guid currentUserId,
            ISender mediator) => {

            var command = new DeleteCommentCommand(
                new CommentId(commentId),
                new UserId(currentUserId));

            ErrorOr<DeleteCommentResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => ProblemExtensions.CreateProblemDetails(errors));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.Comments)
        .RequireAuthorization();
    }
}