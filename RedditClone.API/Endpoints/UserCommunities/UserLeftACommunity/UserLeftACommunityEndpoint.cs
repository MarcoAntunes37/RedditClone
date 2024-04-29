namespace RedditClone.API.Endpoints.UserCommunities.UserLeftACommunity;

using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedditClone.API.Extension;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.UserCommunities.Commands.UserLeftACommunity;
using RedditClone.Application.UserCommunities.Results.UserJoinACommunityResults;

public class UserLeftACommunityEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/user-communities/user-left-community/", async (
            [FromBody]UserLeftACommunityRequest req,
            ISender mediator) =>
        {
            var command = new UserLeftACommunityCommand(
                new CommunityId(req.CommunityId),
                new UserId(req.UserId));

            ErrorOr<UserLeftACommunityResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => ProblemExtensions.CreateProblemDetails(errors));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.UserCommunities)
        .RequireAuthorization();
    }
}