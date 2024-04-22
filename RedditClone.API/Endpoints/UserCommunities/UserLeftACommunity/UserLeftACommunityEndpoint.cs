using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.UserCommunities.Commands.UserLeftACommunity;
using RedditClone.Application.UserCommunities.Results.UserJoinACommunityResults;

namespace RedditClone.API.Endpoints.UserCommunities.UserLeftACommunity;

public class UserLeftACommunityEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/user-left-community/", async (
            [FromBody]UserLeftACommunityRequest req,
            ISender mediator) =>
        {
            var command = new UserLeftACommunityCommand(
                new CommunityId(req!.CommunityId),
                new UserId(req.CurrentUserId));

            ErrorOr<UserLeftACommunityResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => Results.Problem(
                    result.FirstError.Code,
                    result.FirstError.Description));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.UserCommunities);
    }
}