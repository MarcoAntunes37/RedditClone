namespace RedditClone.API.Endpoints.Post.GetPostById;

using ErrorOr;
using MediatR;
using RedditClone.API.Extension;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Application.Post.Queries.GetPostById;
using RedditClone.Application.Post.Results.GetPostByIdResult;

public class GetPostByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/posts/{postId}", async (
            Guid postId,
            ISender mediator) =>
        {
            var query = new GetPostByIdQuery(new PostId(postId));

            ErrorOr<GetPostByIdResult> result = await mediator.Send(query);

            return result.Match(
                result => Results.Ok(result),
                errors => ProblemExtensions.CreateProblemDetails(errors));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.Posts)
        .RequireAuthorization();
    }
}