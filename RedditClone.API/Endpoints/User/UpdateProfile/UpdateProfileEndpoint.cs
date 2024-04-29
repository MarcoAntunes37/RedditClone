namespace RedditClone.API.Endpoints.User.UpdateProfile;

using ErrorOr;
using MediatR;
using RedditClone.API.Extension;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Application.User.Commands.UpdateProfile;
using RedditClone.Application.User.Results.UpdateProfile;

public class UpdateProfileEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/users/{userId}/update-profile", async (
            Guid userId,
            UpdateProfileRequest req,
            ISender mediator) =>
        {
            var command = new UpdateProfileCommand(
                new UserId(userId),
                req.Firstname,
                req.Lastname,
                req.Email);

            ErrorOr<UpdateProfileResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => ProblemExtensions.CreateProblemDetails(errors)
            );
        })
        .MapToApiVersion(1)
        .WithTags(Tags.Users)
        .RequireAuthorization();
    }
}