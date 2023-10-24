namespace ListTodo.API.Controllers;

using ErrorOr;
using ListTodo.Contracts.Authentication;
using ListToDo.API.Controllers;
using ListToDo.Application.Services.Authentication;
using ListToDo.Application.Services.Authentication.Responses;
using Microsoft.AspNetCore.Mvc;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {

        ErrorOr<RegisterResponse> result = _authenticationService.Register(
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
        ErrorOr<LoginResponse> result = _authenticationService.Login(
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