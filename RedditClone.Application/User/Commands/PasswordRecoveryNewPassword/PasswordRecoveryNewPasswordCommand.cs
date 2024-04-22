namespace RedditClone.Application.User.Commands.PasswordRecoveryNewPassword;

using ErrorOr;
using MediatR;
using RedditClone.Application.User.Results.PasswordRecoveryNewPassword;

public record PasswordRecoveryNewPasswordCommand(
    string Email,
    string NewPassword,
    string ConfirmPassword
) : IRequest<ErrorOr<PasswordRecoveryNewPasswordResult>>;