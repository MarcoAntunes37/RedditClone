namespace RedditClone.API.Endpoints.User.Register;

using ErrorOr;
using MediatR;
using RedditClone.Application.User.Commands.Register;
using RedditClone.Application.User.Results.Register;

public class RegisterEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/users/register", async (
            RegisterRequest request,
            ISender mediator) =>
        {
            var command = new RegisterCommand(
                request.Firstname,
                request.Lastname,
                request.Username,
                request.Email,
                request.Password,
                request.RepeatPassword);

            ErrorOr<RegisterResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => Results.Problem(
                    result.FirstError.Code,
                    result.FirstError.Description));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.Users);
    }
}