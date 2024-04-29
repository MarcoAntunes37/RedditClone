namespace RedditClone.API.Endpoints.User.Login;

using ErrorOr;
using MediatR;
using RedditClone.API.Extension;
using RedditClone.Application.User.Queries.Login;
using RedditClone.Application.User.Results.Login;

public class LoginEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/users/login", async (
            LoginRequest request,
            ISender mediator) =>
        {
            var query = new LoginQuery(
                request.Email,
                request.Password);

            ErrorOr<LoginResult> result = await mediator.Send(query);

            return result.Match(
                result => Results.Ok(result),
                errors => ProblemExtensions.CreateProblemDetails(errors));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.Users);
    }
}