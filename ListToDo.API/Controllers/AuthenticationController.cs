namespace ListTodo.API.Controllers;

using ListTodo.Contracts.Authentication;
using ListToDo.Application.Services.Authentication;
using ListToDo.Application.Services.Authentication.Responses;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request){

        var result = _authenticationService.Register(
            request.FirstName, 
            request.LastName, 
            request.Email, 
            request.Password
        );

        var response = new RegisterResponse(
            result.Id,
            result.FirstName,
            result.LastName,
            result.Email
        );

        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request){
         var result = _authenticationService.Login(            
            request.Email, 
            request.Password
        );

        var response = new LoginResponse(
            result.Id,
            result.Email,
            "token"
        );

        return Ok(response);
    }
}