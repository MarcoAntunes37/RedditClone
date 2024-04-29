
using ErrorOr;
using MediatR;
using RedditClone.API.Extension;
using RedditClone.Application.Post.Commands.UpdatePost;
using RedditClone.Application.Post.Results.UpdatePostResult;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

namespace RedditClone.API.Endpoints.Post.UpdatePost;

public class UpdatePostEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/posts/{postId}/update", async (
            Guid postId,
            UpdatePostRequest req,
            ISender mediator) =>
        {
            var command = new UpdatePostCommand(
                new PostId(postId),
                new UserId(req!.UserId),
                req.Title,
                req.Content);

            ErrorOr<UpdatePostResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => ProblemExtensions.CreateProblemDetails(errors));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.Posts)
        .RequireAuthorization();
    }
}