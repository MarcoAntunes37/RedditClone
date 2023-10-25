namespace ListTodo.API.Controllers;

using ErrorOr;
using ListTodo.Contracts.Authentication;
using ListToDo.API.Controllers;
using ListToDo.Application.Services.Authentication.Commands;
using ListToDo.Application.Services.Authentication.Queries;
using ListToDo.Application.Services.Authentication.Responses;
using Microsoft.AspNetCore.Mvc;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationCommandsService _authenticationCommandsService;
    private readonly IAuthenticationQueriesService _authenticationQueriesService;

    public AuthenticationController(IAuthenticationCommandsService authenticationCommandsService,
    IAuthenticationQueriesService authenticationQueriesService)
    {
        _authenticationCommandsService = authenticationCommandsService;
        _authenticationQueriesService = authenticationQueriesService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {

        ErrorOr<RegisterResponse> result = _authenticationCommandsService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );

        return result.Match(
            result => Ok(MapRegisterResult(result)),
            errors => Problem(errors)
        );
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        ErrorOr<LoginResponse> result = _authenticationQueriesService.Login(
           request.Email,
           request.Password
       );

        return result.Match(
            result => Ok(MapAuthResult(result)),
            errors => Problem(errors));
    }

    private static RegisterResponse MapRegisterResult(RegisterResponse result)
    {
        return new RegisterResponse(
                    result.Id,
                    result.FirstName,
                    result.LastName,
                    result.Email
                );
    }

    private static LoginResponse MapAuthResult(LoginResponse result)
    {
        return new LoginResponse(
            result.Id,
            result.Email,
            result.Token
        );
    }
}