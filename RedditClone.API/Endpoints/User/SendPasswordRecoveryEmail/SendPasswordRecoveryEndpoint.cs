namespace RedditClone.API.Endpoints.User.SendPasswordRecoveryEmail;

using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Routing;
using RedditClone.Application.User.Commands.SendPasswordRecoveryEmail;
using RedditClone.Application.User.Results.SendPasswordRecoveryEmail;

public class SendPasswordRecoveryEmailEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/users/password-recovery/", async (
            SendPasswordRecoveryRequest request,
            ISender mediator) =>
        {
            var command = new SendPasswordRecoveryEmailCommand(
                request.Email
            );

            ErrorOr<SendPasswordRecoveryEmailResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => Results.Problem(
                    errors.First().Code,
                    errors.First().Description));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.Users);
    }
}