namespace RedditClone.Application.User.Commands.PasswordRecoveryCodeValidate;

using MediatR;
using Serilog;
using ErrorOr;
using RedditClone.Application.Common.Interfaces.Services;
using RedditClone.Application.User.Results.PasswordRecoveryCodeValidate;

public partial class PasswordRecoveryCodeValidateCommandHandler(
    IRecoveryCodeManager recoveryCodeManager)
: IRequestHandler<PasswordRecoveryCodeValidateCommand, ErrorOr<PasswordRecoveryCodeValidateResult>>
{
    private readonly IRecoveryCodeManager _recoveryCodeManager = recoveryCodeManager;

    public async Task<ErrorOr<PasswordRecoveryCodeValidateResult>> Handle(
        PasswordRecoveryCodeValidateCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@PasswordRecoveryCodeValidateCommand}",
            "Trying to validate code {@Code}",
            command.Code);

        PasswordRecoveryCodeValidateResult result = new (
            _recoveryCodeManager.ValidateCode(command.Email, command.Code),
            command.Email);

        Log.Information(
            "{@PasswordRecoveryCodeValidateResult}, {@Code}, {@Email}",
            result,
            command.Code,
            command.Email);

        return result;
    }
}