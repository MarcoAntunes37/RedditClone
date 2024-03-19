namespace RedditClone.API.Controllers;

using MediatR;
using RedditClone.Contracts.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RedditClone.Application.User.Results.Register;
using RedditClone.Application.User.Results.Login;
using RedditClone.Contracts.Register;
using RedditClone.Application.User.Results;
using RedditClone.API.Mappers;
using RedditClone.Contracts.UpdateProfile;
using RedditClone.Application.User.Results.UpdateProfile;
using RedditClone.Contracts.UpdatePassword;
using RedditClone.Application.User.Results.UpdatePassword;
using RedditClone.Contracts.SendPasswordRecoveryEmail;
using RedditClone.Application.User.Results.SendPasswordRecoveryEmail;
using RedditClone.Contracts.PasswordRecoveryCodeValidate;
using RedditClone.Application.User.Results.PasswordRecoveryCodeValidate;
using RedditClone.Contracts.PasswordRecoveryNewPassword;
using RedditClone.Application.User.Results.PasswordRecoveryNewPassword;

[Route("user")]
[AllowAnonymous]
public class UserController : ApiController
{
    private readonly ISender _sender;

    public UserController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = UserMappers.MapRegisterRequest(request);

        RegisterResult result = await _sender.Send(command);

        return Ok(UserMappers.MapRegisterResult(result));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = UserMappers.MapLoginRequest(request);

        LoginResult result = await _sender.Send(query);

        return Ok(UserMappers.MapLoginResult(result));
    }

    [HttpDelete("delete/{userId}")]
    public async Task<IActionResult> Delete(
        [FromRoute]Guid userId
    )
    {
        var command = UserMappers.MapDeleteUserRequest(userId);

        DeleteResult result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpPut("update-profile/{userId}")]
    public async Task<IActionResult> UpdateProfile(
        [FromRoute]Guid userId,
        UpdateProfileRequest request
    )
    {
        var command = UserMappers.MapUpdateProfileRequest(userId, request);

        UpdateProfileResult result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpPut("update-password/{userId}")]
    public async Task<IActionResult> UpdatePassword(
        [FromRoute]Guid userId,
        UpdatePasswordRequest request
    )
    {
        var command = UserMappers.MapUpdatePasswordRequest(userId, request);

        UpdatePasswordResult result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpPost("password-recovery")]
    public async Task<IActionResult> SendPasswordRecoveryEmail(
        SendPasswordRecoveryEmailRequest request)
    {
        var command = UserMappers.MapSendPasswordRecoveryEmailRequest(request);

        SendPasswordRecoveryEmailResult result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpPost("password-recovery/validate")]
    public async Task<IActionResult> ValidatePasswordRecoveryCode(
        PasswordRecoveryCodeValidateRequest request)
    {
        var command = UserMappers.MapPasswordRecoveryCodeValidateRequest(request);

        PasswordRecoveryCodeValidateResult result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpPost("password-recovery/new-password")]
    public async Task<IActionResult> NewPasswordRecovery(
        PasswordRecoveryNewPasswordRequest request)
    {
        var command = UserMappers.MapNewPasswordRecoveryRequest(request);

        PasswordRecoveryNewPasswordResult result = await _sender.Send(command);

        return Ok(result);
    }
}