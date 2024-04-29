namespace RedditClone.API.Endpoints.Community.GetCommunityById;

using MediatR;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.Community.Queries.GetCommunitiesById;
using RedditClone.Application.Community.Results.GetCommunityByIdResult;

public class GetCommunityByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/communities/{communityId}", async (
            Guid communityId,
            ISender mediator) =>
        {
            var query = new GetCommunityByIdQuery(new CommunityId(communityId));

            GetCommunityByIdResult result = await mediator.Send(query);

            return Results.Ok(result);
        })
        .MapToApiVersion(1)
        .WithTags(Tags.Communities)
        .RequireAuthorization();
    }
}
