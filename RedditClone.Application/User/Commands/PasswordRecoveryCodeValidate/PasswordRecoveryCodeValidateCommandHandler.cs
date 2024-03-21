namespace RedditClone.Application.User.Commands.PasswordRecoveryCodeValidate;

using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Common.Interfaces.Services;
using RedditClone.Application.Helpers;
using RedditClone.Application.User.Results.PasswordRecoveryCodeValidate;
using Serilog;

public partial class PasswordRecoveryCodeValidateCommandHandler
    : IRequestHandler<PasswordRecoveryCodeValidateCommand, PasswordRecoveryCodeValidateResult>
{
    private readonly IRecoveryCodeManager _recoveryCodeManager;
    private readonly IValidator<PasswordRecoveryCodeValidateCommand> _validator;
    private readonly IConfiguration _configuration;
    public PasswordRecoveryCodeValidateCommandHandler(
        IRecoveryCodeManager recoveryCodeManager,
        IValidator<PasswordRecoveryCodeValidateCommand> validator,
        IConfiguration configuration)
    {
        _recoveryCodeManager = recoveryCodeManager;
        _validator = validator;
        _configuration = configuration;
    }

    public async Task<PasswordRecoveryCodeValidateResult> Handle(PasswordRecoveryCodeValidateCommand command,
    CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@PasswordRecoveryCodeValidateCommand}",
            "Trying to validate code {@Code}",
            command.Code);

        _validator.ValidateAndThrow(command);

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