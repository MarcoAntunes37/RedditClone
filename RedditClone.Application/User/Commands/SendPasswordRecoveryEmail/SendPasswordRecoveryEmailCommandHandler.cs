namespace RedditClone.Application.User.Commands.SendPasswordRecoveryEmail;

using System.Net;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Common.Interfaces.Services;
using RedditClone.Application.Common.Errors;
using RedditClone.Application.Common.Helpers;
using RedditClone.Application.Persistence;
using RedditClone.Application.User.Results.SendPasswordRecoveryEmail;
using Serilog;

public partial class SendPasswordRecoveryEmailCommandHandler
    : IRequestHandler<SendPasswordRecoveryEmailCommand, SendPasswordRecoveryEmailResult>
{
    private readonly IEmailRecovery _emailRecovery;
    private readonly IRecoveryCodeManager _recoveryCodeManager;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<SendPasswordRecoveryEmailCommand> _validator;
    private readonly IConfiguration _configuration;
    public SendPasswordRecoveryEmailCommandHandler(
        IEmailRecovery emailRecovery,
        IRecoveryCodeManager recoveryCodeManager,
        IValidator<SendPasswordRecoveryEmailCommand> validator,
        IUserRepository userRepository,
        IConfiguration configuration)
    {
        _emailRecovery = emailRecovery;
        _recoveryCodeManager = recoveryCodeManager;
        _validator = validator;
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<SendPasswordRecoveryEmailResult> Handle(SendPasswordRecoveryEmailCommand command,
    CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@SendPasswordEmailRecoveryCommand}",
            "Trying to send email to {@Email}",
            command,
            command.Email);

        _validator.ValidateAndThrow(command);

        if(_userRepository.GetUserByEmail(command.Email) is null){
            throw new HttpCustomException(
            HttpStatusCode.NotFound, $"Cannot found a user with this email {command.Email}");
        }

        Random random = new();

        var code =
            random.Next(100000, 999999).ToString();

        DateTime expiration = DateTime.UtcNow.AddMinutes(5);

        _recoveryCodeManager.AddCode(command.Email, code, expiration);

        var body = $@"This is the RedditClone recovery email.
            You recovery code is: {code}.
            If you did not request the recovery email, please disregard it.";

        _emailRecovery.SendEmail(
            command.Email,
            "RedditClone Email recovery",
            body);

        SendPasswordRecoveryEmailResult result = new(
            $"Recovery email sent to {command.Email}, check spam and deleted emails");

        Log.Information(
            "{@SendPasswordRecoveryEmailResult}, {@Email}",
            result,
            command.Email);

        return result;
    }
}