namespace RedditClone.API.Endpoints.User.UpdatePassword;

using ErrorOr;
using MediatR;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Application.User.Commands.UpdatePassword;
using RedditClone.Application.User.Results.UpdatePassword;

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
                errors => Results.Problem(
                    result.FirstError.Code,
                    result.FirstError.Description)
            );
        })
        .MapToApiVersion(1)
        .WithTags(Tags.Users);
    }
}