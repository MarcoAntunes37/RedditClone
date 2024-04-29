namespace RedditClone.API.Endpoints.Post.GetPostsListByCommunityId;

using MediatR;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.Community.Queries.GetPostListByCommunityId;

public class GetPostsListsByCommunityIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/communities/{communityId}/posts", async (
            Guid communityId,
            int page,
            int pageSize,
            ISender mediator) =>
        {

            var query = new GetPostListByCommunityIdQuery(
                new CommunityId(communityId),
                page,
                pageSize);

            var result = await mediator.Send(query);

            return Results.Ok(result);
        })
        .MapToApiVersion(1)
        .WithTags(Tags.Posts)
        .RequireAuthorization();
    }
}