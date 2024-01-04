namespace RedditClone.API.Controllers;

using ErrorOr;
using RedditClone.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using RedditClone.Application.Authentication.Commands.Register;
using MediatR;
using RedditClone.Application.Authentication.Results.Register;
using RedditClone.Application.Authentication.Results.Login;
using RedditClone.Application.Authentication.Queries.Login;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly ISender _sender;

    public AuthenticationController(
        ISender sender
    )
    {
        _sender = sender;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
        ErrorOr<RegisterResult> result = await _sender.Send(command);

        return result.Match(
            result => Ok(MapRegisterResult(result)),
            errors => Problem(errors)
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(request.Email, request.Password);
        ErrorOr<LoginResult> result = await _sender.Send(query);

        return result.Match(
            result => Ok(MapAuthResult(result)),
            errors => Problem(errors));
    }

    private static RegisterResult MapRegisterResult(RegisterResult result)
    {
        return new RegisterResult(
                    result.Id,
                    result.FirstName,
                    result.LastName,
                    result.Email
                );
    }

    private static LoginResult MapAuthResult(LoginResult result)
    {
        return new LoginResult(
            result.Email,
            result.Token
        );
    }
}