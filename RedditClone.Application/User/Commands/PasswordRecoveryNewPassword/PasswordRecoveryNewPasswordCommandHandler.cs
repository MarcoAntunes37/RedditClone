namespace RedditClone.Application.User.Commands.PasswordRecoveryNewPassword;

using ErrorOr;
using MediatR;
using Serilog;
using RedditClone.Application.Persistence;
using RedditClone.Application.User.Results.PasswordRecoveryNewPassword;

public partial class PasswordRecoveryNewPasswordCommandHandler(
    IUserRepository userRepository)
        : IRequestHandler<PasswordRecoveryNewPasswordCommand,
        ErrorOr<PasswordRecoveryNewPasswordResult>>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ErrorOr<PasswordRecoveryNewPasswordResult>> Handle(
        PasswordRecoveryNewPasswordCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@PasswordRecoveryNewPasswordCommand}",
            "Trying to update password of user with {@Email}",
            command,
            command.Email);

        _userRepository.UpdateRecoveredPassword(command.Email, command.NewPassword, command.ConfirmPassword);

        PasswordRecoveryNewPasswordResult result = new("Password updated successfully");

        Log.Information(
            "{@PasswordRecoveryNewPasswordResult}, {@Email}",
            result,
            command.Email);

        return result;
    }
}