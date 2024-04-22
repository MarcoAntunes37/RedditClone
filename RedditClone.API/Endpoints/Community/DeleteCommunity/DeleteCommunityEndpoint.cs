namespace RedditClone.API.Endpoints.Community.DeleteCommunity;

using ErrorOr;
using MediatR;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.Community.Commands.DeleteCommunity;
using RedditClone.Application.Community.Results.DeleteCommunityResult;
using RedditClone.Domain.UserCommunitiesAggregate.Enum;
using Microsoft.AspNetCore.Mvc;

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
                new UserId(req.UserId),
                (Role)req.Role);

            ErrorOr<DeleteCommunityResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => Results.Problem(
                    errors.First().Code,
                    errors.First().Description
                )
            );
        })
        .MapToApiVersion(1)
        .WithTags(Tags.Communities);
    }
}