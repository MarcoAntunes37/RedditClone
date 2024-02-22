namespace RedditClone.API.Mappers;

using RedditClone.Contracts.Register;
using RedditClone.Contracts.Login;
using RedditClone.Application.User.Commands.Register;
using RedditClone.Application.User.Commands.Delete;
using RedditClone.Application.User.Queries.Login;
using RedditClone.Application.User.Results.Register;
using RedditClone.Application.User.Results.Login;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Application.User.Commands.UpdateProfile;
using RedditClone.Contracts.UpdateProfile;
using RedditClone.Application.User.Commands.UpdatePassword;
using RedditClone.Contracts.UpdatePassword;
using RedditClone.Application.User.Commands.SendPasswordRecoveryEmail;
using RedditClone.Contracts.SendPasswordRecoveryEmail;
using RedditClone.Contracts.PasswordRecoveryCodeValidate;
using RedditClone.Application.User.Commands.PasswordRecoveryCodeValidate;
using RedditClone.Contracts.PasswordRecoveryNewPassword;
using RedditClone.Application.User.Commands.PasswordRecoveryNewPassword;

public static class UserMappers
{
    public static RegisterCommand MapRegisterRequest(RegisterRequest request)
    {
        return new RegisterCommand(
            new UserId(Guid.NewGuid()),
            request.Firstname,
            request.Lastname,
            request.Username,
            request.Password,
            request.Email,
            DateTime.UtcNow,
            DateTime.UtcNow
        );
    }

    public static LoginQuery MapLoginRequest(LoginRequest request)
    {
        return new LoginQuery(
            request.Email,
            request.Password
        );
    }

    public static LoginResponse MapLoginResult(LoginResult result)
    {
        return new LoginResponse(
            result.Token
        );
    }

    public static RegisterResponse MapRegisterResult(RegisterResult result)
    {
        var user = result.User;
        return new RegisterResponse(
            user.Firstname,
            user.Lastname,
            user.Email,
            user.Username,
            user.Password,
            user.CreatedAt,
            user.UpdatedAt,
            result.Token
        );
    }

    public static DeleteUserCommand MapDeleteUserRequest(Guid userId)
    {
        return new DeleteUserCommand(
            new UserId(userId));
    }

    public static UpdateProfileCommand MapUpdateProfileRequest(
        Guid userId,
        UpdateProfileRequest request)
    {
        return new UpdateProfileCommand(
            new UserId(userId),
            request.Firstname,
            request.Lastname,
            request.Email
        );
    }

    public static UpdatePasswordCommand MapUpdatePasswordRequest(
        Guid userId,
        UpdatePasswordRequest request)
    {
        return new UpdatePasswordCommand(
            new UserId(userId),
            request.OldPassword,
            request.NewPassword);
    }

    public static SendPasswordRecoveryEmailCommand MapSendPasswordRecoveryEmailRequest(
        SendPasswordRecoveryEmailRequest request)
    {
        return new SendPasswordRecoveryEmailCommand(
           request.Email);
    }

    public static PasswordRecoveryCodeValidateCommand MapPasswordRecoveryCodeValidateRequest(
        PasswordRecoveryCodeValidateRequest request)
    {
        return new PasswordRecoveryCodeValidateCommand(
           request.Code,
           request.Email);
    }

    public static PasswordRecoveryNewPasswordCommand MapNewPasswordRecoveryRequest(
        PasswordRecoveryNewPasswordRequest request)
    {
        return new PasswordRecoveryNewPasswordCommand(
            request.Email,
            request.NewPassword
           );
    }
}
