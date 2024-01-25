namespace RedditClone.API.Controllers;

using ErrorOr;
using MediatR;
using RedditClone.Contracts.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RedditClone.Application.Authentication.Commands.Register;
using RedditClone.Application.Authentication.Results.Register;
using RedditClone.Application.Authentication.Queries.Login;
using RedditClone.Application.Authentication.Results.Login;
using RedditClone.Contracts.Register;
using RedditClone.Contracts.Authentication;
using RedditClone.Domain.UserAggregate.Entities;
using RedditClone.Domain.UserAggregate.ValueObjects;

[Route("auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly ISender _sender;
    public AuthenticationController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = MapRegisterRequest(request);
        ErrorOr<RegisterResult> result = await _sender.Send(command);

        return result.Match(
            result => Ok(MapRegisterResult(result)),
            errors => Problem(errors)
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = MapLoginRequest(request);
        ErrorOr<LoginResult> result = await _sender.Send(query);

        return result.Match(
            result => Ok(MapLoginResult(result)),
            errors => Problem(errors)
        );
    }

    private static LoginQuery MapLoginRequest(LoginRequest request)
    {
        return new LoginQuery(
            request.Email,
            request.Password
        );
    }

    private static LoginResponse MapLoginResult(LoginResult result)
    {
        return new LoginResponse(
            result.Token
        );
    }

    private static RegisterCommand MapRegisterRequest(RegisterRequest request)
    {
        return new RegisterCommand(
            UserId.CreateUnique(),
            request.FirstName,
            request.LastName,
            request.Username,
            request.Password,
            request.Email,
            request.CreatedAt,
            request.UpdatedAt
        );
    }

    private static RegisterResponse MapRegisterResult(RegisterResult result)
    {
        return new RegisterResponse(
            result.User.FirstName,
            result.User.LastName,
            result.User.Email,
            result.User.Username,
            result.User.Password,
            result.User.CreatedAt,
            result.User.UpdatedAt,
            result.Token
        );
    }
}