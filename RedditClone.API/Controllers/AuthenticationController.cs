namespace RedditClone.API.Controllers;

using ErrorOr;
using RedditClone.Contracts.Login;
using Microsoft.AspNetCore.Mvc;
using RedditClone.Application.Authentication.Commands.Register;
using MediatR;
using RedditClone.Application.Authentication.Results.Register;
using RedditClone.Application.Authentication.Results.Login;
using RedditClone.Application.Authentication.Queries.Login;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using RedditClone.Contracts.Register;

[Route("auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public AuthenticationController(
        ISender sender,
        IMapper mapper
    )
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        ErrorOr<RegisterResult> result = await _sender.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<RegisterResult>(result)),
            errors => Problem(errors)
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);
        ErrorOr<LoginResult> result = await _sender.Send(query);

        return result.Match(
            result => Ok(_mapper.Map<LoginResult>(result)),
            errors => Problem(errors));
    }
}