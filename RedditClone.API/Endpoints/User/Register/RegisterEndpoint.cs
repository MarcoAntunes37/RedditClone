namespace RedditClone.API.Endpoints.User.Register;

using ErrorOr;
using MediatR;
using RedditClone.API.Extension;
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
                request.Password,
                request.RepeatPassword,
                request.Email);

            ErrorOr<RegisterResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => ProblemExtensions.CreateProblemDetails(errors));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.Users);
    }
}