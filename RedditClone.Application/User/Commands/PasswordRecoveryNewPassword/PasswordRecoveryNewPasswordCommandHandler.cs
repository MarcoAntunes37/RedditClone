namespace RedditClone.Application.User.Commands.PasswordRecoveryNewPassword;

using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Common.Helpers;
using RedditClone.Application.Persistence;
using RedditClone.Application.User.Results.PasswordRecoveryNewPassword;
using Serilog;

public partial class PasswordRecoveryNewPasswordCommandHandler
    : IRequestHandler<PasswordRecoveryNewPasswordCommand,
        PasswordRecoveryNewPasswordResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<PasswordRecoveryNewPasswordCommand> _validator;
    private readonly IConfiguration _configuration;
    public PasswordRecoveryNewPasswordCommandHandler(
        IUserRepository userRepository,
        IValidator<PasswordRecoveryNewPasswordCommand> validator,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _validator = validator;
        _configuration = configuration;
    }

    public async Task<PasswordRecoveryNewPasswordResult> Handle(PasswordRecoveryNewPasswordCommand command,
    CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@PasswordRecoveryNewPasswordCommand}",
            "Trying to update password of user with {@Email}",
            command,
            command.Email);

        _validator.ValidateAndThrow(command);

        _userRepository.UpdateRecoveredPassword(command.Email, command.NewPassword, command.ConfirmPassword);

        PasswordRecoveryNewPasswordResult result = new("Password updated successfully");

        Log.Information(
            "{@PasswordRecoveryNewPasswordResult}, {@Email}",
            result,
            command.Email);

        return result;
    }
}