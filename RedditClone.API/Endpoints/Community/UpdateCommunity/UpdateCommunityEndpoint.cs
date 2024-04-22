namespace RedditClone.API.Endpoints.Community.UpdateCommunity;

using ErrorOr;
using MediatR;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.Community.Commands.UpdateCommunity;
using RedditClone.Application.Community.Results.UpdateCommunityResult;

public class UpdateCommunityEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/communities/{communityId}/update", async (
            Guid communityId,
            UpdateCommunityRequest req,
            ISender mediator) =>
        {
            var command = new UpdateCommunityCommand(
                new CommunityId(communityId),
                new UserId(req.UserId),
                req.Name,
                req.Description,
                req.Topic);

            ErrorOr<UpdateCommunityResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => Results.Problem(
                    errors.First().Code,
                    errors.First().Description));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.Communities);
    }
}