namespace RedditClone.Application.User.Commands.SendPasswordRecoveryEmail;

using MediatR;
using Serilog;
using ErrorOr;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Persistence;
using RedditClone.Application.Common.Interfaces.Services;
using RedditClone.Application.User.Results.SendPasswordRecoveryEmail;

public partial class SendPasswordRecoveryEmailCommandHandler(
    IEmailService emailService,
    IRecoveryCodeManager recoveryCodeManager,
    IUserRepository userRepository)
        : IRequestHandler<SendPasswordRecoveryEmailCommand, ErrorOr<SendPasswordRecoveryEmailResult>>
{
    private readonly IEmailService _emailService = emailService;
    private readonly IRecoveryCodeManager _recoveryCodeManager = recoveryCodeManager;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ErrorOr<SendPasswordRecoveryEmailResult>> Handle(
        SendPasswordRecoveryEmailCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@SendPasswordEmailRecoveryCommand}",
            "Trying to send email to {@Email}",
            command,
            command.Email);

        if (_userRepository.GetUserByEmail(command.Email) is null)
        {
            return Errors.User.UserNotFound;
        }

        Random random = new();

        var code =
            random.Next(100000, 999999).ToString();

        DateTime expiration = DateTime.UtcNow.AddMinutes(5);

        _recoveryCodeManager.AddCode(command.Email, code, expiration);

        var body = $@"This is the RedditClone recovery email.
            You recovery code is: {code}.
            If you did not request the recovery email, please disregard it.";

        await _emailService.SendRecoveryEmailAsync(
            command.Email, "RedditClone Email recovery", body, cancellationToken);

        SendPasswordRecoveryEmailResult result = new(
            $"Recovery email sent to {command.Email}, check spam and deleted emails");

        Log.Information(
            "{@SendPasswordRecoveryEmailResult}, {@Email}",
            result,
            command.Email);

        return result;
    }
}