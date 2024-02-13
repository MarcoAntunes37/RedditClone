namespace RedditClone.Application.User.Commands.PasswordRecoveryNewPassword;

using MediatR;
using RedditClone.Application.User.Results.PasswordRecoveryNewPassword;

public record PasswordRecoveryNewPasswordCommand(
    string Email,
    string NewPassword
) : IRequest<PasswordRecoveryNewPasswordResult>;