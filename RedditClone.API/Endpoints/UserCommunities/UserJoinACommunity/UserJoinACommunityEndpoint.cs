namespace RedditClone.API.Endpoints.UserCommunities.UserJoinACommunity;

using ErrorOr;
using MediatR;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.UserCommunitiesAggregate.Enum;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.UserCommunities.Commands.UserJoinACommunity;
using RedditClone.Application.UserCommunities.Results.UserJoinACommunityResults;


public class UserJoinACommunityEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/user-join-community/", async (
            UserJoinACommunityRequest req,
            ISender mediator) =>
        {
            var command = new UserJoinACommunityCommand(
                new CommunityId(req.CommunityId),
                new UserId(req.CurrentUserId),
                (Role)req.Role);

            ErrorOr<UserJoinACommunityResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result), errors =>
                    Results.Problem(
                        result.FirstError.Code,
                        result.FirstError.Description));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.UserCommunities);
    }
}