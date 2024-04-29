namespace RedditClone.API.Endpoints.UserCommunities.UserCommunityRoleUpdate;

using ErrorOr;
using MediatR;
using RedditClone.API.Extension;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.UserCommunitiesAggregate.Enum;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.UserCommunities.Commands.UserCommunityRoleUpdate;
using RedditClone.Application.UserCommunities.Results.UserCommunityRoleUpdateResult;

public class UserCommunityRoleUpdateEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/user-communities/role-update", async (
            UserCommunityRoleUpdateRequest req,
            ISender mediator) =>
        {
            var command = new UserCommunityRoleUpdateCommand(
                new UserId(req.RequesterId),
                new CommunityId(req.CommunityId),
                new UserId(req.UserId),
                (Role)req.Role);

            ErrorOr<UserCommunityRoleUpdateResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => ProblemExtensions.CreateProblemDetails(errors));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.UserCommunities)
        .RequireAuthorization();
    }
}