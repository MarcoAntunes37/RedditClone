namespace RedditClone.API.Endpoints.UserCommunities.GetUsersListByCommunityId;

using MediatR;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.UserCommunities.Queries.GetUserListByCommunityId;

public class GetUsersListByCommunityIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/user-communities/{communityId}/list-users", async (
            Guid communityId,
            int page,
            int pageSize,
            ISender mediator) =>
        {

            var query = new GetUserListByCommunityIdQuery(
                new CommunityId(communityId),
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