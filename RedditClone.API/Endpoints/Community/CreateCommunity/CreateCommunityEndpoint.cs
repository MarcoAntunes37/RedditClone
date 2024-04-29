namespace RedditClone.API.Endpoints.Community.CreateCommunity;

using ErrorOr;
using MediatR;
using RedditClone.API.Extension;
using RedditClone.Application.Community.Commands.CreateCommunity;
using RedditClone.Application.Community.Results.CreateCommunityResult;
using RedditClone.Domain.UserAggregate.ValueObjects;

public class CreateCommunityEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/communities/create", async (
            CreateCommunityRequest req,
            ISender mediator) =>
        {
            var command = new CreateCommunityCommand(
                req.Name,
                req.Description,
                req.Topic,
                new UserId(req.OwnerId));

            ErrorOr<CreateCommunityResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => ProblemExtensions.CreateProblemDetails(errors));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.Communities)
        .RequireAuthorization();
    }
}
