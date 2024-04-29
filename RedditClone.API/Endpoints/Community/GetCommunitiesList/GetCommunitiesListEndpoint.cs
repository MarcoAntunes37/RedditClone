namespace RedditClone.API.Endpoints.Community.GetCommunitiesList;

using MediatR;
using RedditClone.Application.Community.Queries.GetCommunitiesList;
using RedditClone.Application.Community.Results.GetCommunitiesListResult;

public class GetCommunitiesListEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/communities-list/", async (
            string? name,
            string? topic,
            int page,
            int pageSize,
            ISender mediator) =>
        {
            var query = new GetCommunitiesListQuery(name!, topic!, page, pageSize);

            GetCommunitiesListResult result = await mediator.Send(query);

            return Results.Ok(result);
        })
        .MapToApiVersion(1)
        .WithTags(Tags.Communities)
        .RequireAuthorization();
    }
}