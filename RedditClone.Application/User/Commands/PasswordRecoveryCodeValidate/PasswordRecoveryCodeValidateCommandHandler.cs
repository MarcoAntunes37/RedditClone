namespace RedditClone.Application.User.Commands.PasswordRecoveryCodeValidate;

using MediatR;
using RedditClone.Application.Common.Interfaces.Services;
using RedditClone.Application.User.Results.PasswordRecoveryCodeValidate;

public partial class PasswordRecoveryCodeValidateCommandHandler
    : IRequestHandler<PasswordRecoveryCodeValidateCommand, PasswordRecoveryCodeValidateResult>
{
    private readonly IRecoveryCodeManager _recoveryCodeManager;
    public PasswordRecoveryCodeValidateCommandHandler(IRecoveryCodeManager recoveryCodeManager)
    {
        _recoveryCodeManager = recoveryCodeManager;
    }

    public async Task<PasswordRecoveryCodeValidateResult> Handle(PasswordRecoveryCodeValidateCommand command,
    CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        return new PasswordRecoveryCodeValidateResult(
            _recoveryCodeManager.ValidateCode(command.Email, command.Code)
        );
    }
}