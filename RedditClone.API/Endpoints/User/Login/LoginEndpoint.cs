namespace RedditClone.API.Endpoints.User.Login;

using ErrorOr;
using MapsterMapper;
using MediatR;
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

            var error = new Error();

            if (result.IsError)
            {
                error = result.Errors[0];
            }
            return result.Match(
                result => Results.Ok(result),
                errors => Results.Problem(
                    error.Code,
                    error.Description));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.Users);
    }
}