namespace RedditClone.API.Endpoints.Comment.GetCommentsByPostId;

using MediatR;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Application.Comment.Queries.GetCommentsListByPostId;
using RedditClone.Application.Comment.Results.GetCommentsListByPostIdResults;

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
            var query = new GetCommentsListByPostIdQuery(
                new PostId(postId),
                page,
                pageSize);

            GetCommentsListByPostIdResult result = await mediator.Send(query);

            return Results.Ok(result);
        })
        .MapToApiVersion(1)
        .WithTags(Tags.Comments)
        .RequireAuthorization();
    }
}