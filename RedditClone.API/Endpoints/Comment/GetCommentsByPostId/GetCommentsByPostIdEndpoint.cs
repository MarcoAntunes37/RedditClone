namespace RedditClone.API.Endpoints.Comment.GetCommentsByPostId;

using MediatR;
using RedditClone.Application.Comment.Queries.GetCommentsByPostId;
using RedditClone.Application.Comment.Results.GetCommentsByPostIdResult;
using RedditClone.Domain.PostAggregate.ValueObjects;

public class GetCommentsByPostIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/comments/{postId}", async (
            Guid postId,
            int page,
            int pageSize,
            ISender mediator
        ) =>
        {
            var query = new GetCommentsByPostIdQuery(
                new PostId(postId),
                page,
                pageSize
            );

            GetCommentsByPostIdResult result = await mediator.Send(query);

            return Results.Ok(result);
        })
        .MapToApiVersion(1)
        .WithTags(Tags.Comments);
    }
}