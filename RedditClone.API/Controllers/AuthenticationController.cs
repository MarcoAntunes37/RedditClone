namespace RedditClone.API.Controllers;

using MediatR;
using RedditClone.Contracts.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RedditClone.Application.User.Commands.Register;
using RedditClone.Application.User.Results.Register;
using RedditClone.Application.User.Queries.Login;
using RedditClone.Application.User.Results.Login;
using RedditClone.Contracts.Register;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Contracts.Community;

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
        RegisterResult result = await _sender.Send(command);

        return Ok(MapRegisterResult(result));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = MapLoginRequest(request);
        LoginResult result = await _sender.Send(query);

        return Ok(MapLoginResult(result));
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
            new List<RegisterUserCommunities>(),
            result.Token
        );
    }
}