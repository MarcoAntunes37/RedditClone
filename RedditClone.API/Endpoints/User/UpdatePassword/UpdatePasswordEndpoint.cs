namespace RedditClone.API.Endpoints.User.UpdatePassword;

using ErrorOr;
using MediatR;
using RedditClone.API.Extension;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Application.User.Results.UpdatePassword;
using RedditClone.Application.User.Commands.UpdatePassword;

public class UpdatePasswordEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/users/{userId}/update-password", async (
            Guid userId,
            UpdatePasswordRequest req,
            ISender mediator) =>
        {
            var command = new UpdatePasswordCommand(
                new UserId(userId),
                req.OldPassword,
                req.NewPassword,
                req.MatchPassword);

            ErrorOr<UpdatePasswordResult> result = await mediator.Send(command);

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