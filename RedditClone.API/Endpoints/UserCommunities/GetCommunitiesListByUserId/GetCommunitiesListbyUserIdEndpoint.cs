namespace RedditClone.API.Endpoints.UserCommunities.GetCommunitiesListByUserId;

using MediatR;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Application.UserCommunities.Queries.GetCommunitiesListByUserId;

public class GetCommunitiesListByUserIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/user-communities/{userId}/list-communities", async (
            Guid userId,
            int page,
            int pageSize,
            ISender mediator) =>
        {
            var query = new GetCommunitiesListByUserIdQuery(
                new UserId(userId),
                page,
                pageSize);

            var result = await mediator.Send(query);

            return Results.Ok(result);
        })
        .MapToApiVersion(1)
        .WithTags(Tags.UserCommunities)
        .RequireAuthorization();
    }
}