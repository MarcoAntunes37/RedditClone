namespace RedditClone.API.Endpoints.Community.DeleteCommunity;

using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.UserCommunitiesAggregate.Enum;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.Community.Commands.DeleteCommunity;
using RedditClone.Application.Community.Results.DeleteCommunityResult;
using RedditClone.API.Extension;

public class DeleteCommunityEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/communities/{communityId}/delete", async (
            Guid communityId,
            [FromBody] DeleteCommunityRequest req,
            ISender mediator) =>
        {
            var command = new DeleteCommunityCommand(
                new CommunityId(communityId),
                new UserId(req.UserId));

            ErrorOr<DeleteCommunityResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => ProblemExtensions.CreateProblemDetails(errors));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.Communities)
        .RequireAuthorization();
    }
}