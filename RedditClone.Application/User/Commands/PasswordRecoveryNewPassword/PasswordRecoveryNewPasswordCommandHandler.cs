namespace RedditClone.Application.User.Commands.PasswordRecoveryNewPassword;

using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Application.User.Results.PasswordRecoveryNewPassword;

public partial class PasswordRecoveryNewPasswordCommandHandler
    : IRequestHandler<PasswordRecoveryNewPasswordCommand,
        PasswordRecoveryNewPasswordResult>
{
    private readonly IUserRepository _userRepository;
    public PasswordRecoveryNewPasswordCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<PasswordRecoveryNewPasswordResult> Handle(PasswordRecoveryNewPasswordCommand command,
    CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _userRepository.UpdateRecoveredPassword(command.Email, command.NewPassword);

        return new PasswordRecoveryNewPasswordResult(
            ""
        );
    }
}