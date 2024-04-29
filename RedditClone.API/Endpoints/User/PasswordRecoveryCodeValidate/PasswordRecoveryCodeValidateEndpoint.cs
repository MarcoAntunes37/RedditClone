namespace RedditClone.API.Endpoints.User.PasswordRecoveryCodeValidate;

using ErrorOr;
using MediatR;
using RedditClone.API.Extension;
using RedditClone.Application.User.Commands.PasswordRecoveryCodeValidate;
using RedditClone.Application.User.Results.PasswordRecoveryCodeValidate;

public class PasswordRecoveryCodeValidateEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/users/password-recovery/validate/", async (
            PasswordRecoveryCodeValidateRequest request,
            ISender mediator) =>
            {
                var command = new PasswordRecoveryCodeValidateCommand(
                    request.Code,
                    request.Email);

                ErrorOr<PasswordRecoveryCodeValidateResult> result = await mediator.Send(command);

                return result.Match(
                    result => Results.Ok(result),
                    errors => ProblemExtensions.CreateProblemDetails(errors));
            })
            .MapToApiVersion(1)
            .WithTags(Tags.Users);
    }
}
