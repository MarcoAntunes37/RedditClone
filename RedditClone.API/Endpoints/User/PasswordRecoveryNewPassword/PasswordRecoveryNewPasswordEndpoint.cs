namespace RedditClone.API.Endpoints.User.PasswordRecoveryNewPassword;

using ErrorOr;
using MediatR;
using RedditClone.Application.User.Commands.PasswordRecoveryNewPassword;
using RedditClone.Application.User.Results.PasswordRecoveryNewPassword;


public class PasswordRecoveryNewPasswordEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/users/password-recovery/new-password/", async (
            PasswordRecoveryNewPasswordRequest request,
            ISender mediator) =>
        {

            var command = new PasswordRecoveryNewPasswordCommand(
                request.Email,
                request.NewPassword,
                request.ConfirmPassword);

            ErrorOr<PasswordRecoveryNewPasswordResult> result = await mediator.Send(command);

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
