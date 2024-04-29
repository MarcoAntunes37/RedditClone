namespace RedditClone.API.Endpoints.CommentReplies.GetCommentRepliesList;

using MediatR;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.CommentReplies.Queries.GetCommentRepliesList;
using RedditClone.Application.CommentReplies.Results.GetCommentRepliesListResult;

public class GetCommentRepliesListEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/comments/{commentId}/replies", async (
            Guid commentId,
            int page,
            int pageSize,
            ISender mediator) =>
        {
            var query = new GetCommentRepliesListQuery(
                new CommentId(commentId),
                page,
                pageSize);

            GetCommentRepliesListResult result = await mediator.Send(query);

            return Results.Ok(result);
        })
        .MapToApiVersion(1)
        .WithTags(Tags.CommentReplies)
        .RequireAuthorization();
    }
}