namespace RedditClone.Application.User.Commands.SendPasswordRecoveryEmail;

using System.Net;
using MediatR;
using RedditClone.Application.Common.Interfaces.Services;
using RedditClone.Application.Errors;
using RedditClone.Application.Persistence;
using RedditClone.Application.User.Results.SendPasswordRecoveryEmail;

public partial class SendPasswordRecoveryEmailCommandHandler
    : IRequestHandler<SendPasswordRecoveryEmailCommand, SendPasswordRecoveryEmailResult>
{
    private readonly IEmailRecovery _emailRecovery;
    private readonly IRecoveryCodeManager _recoveryCodeManager;
    private readonly IUserRepository _userRepository;
    public SendPasswordRecoveryEmailCommandHandler(
        IEmailRecovery emailRecovery,
        IRecoveryCodeManager recoveryCodeManager,
        IUserRepository userRepository)
    {
        _emailRecovery = emailRecovery;
        _recoveryCodeManager = recoveryCodeManager;
        _userRepository = userRepository;
    }

    public async Task<SendPasswordRecoveryEmailResult> Handle(SendPasswordRecoveryEmailCommand command,
    CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

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

        return new SendPasswordRecoveryEmailResult(
            $"Recovery email sent to {command.Email}, check spam and deleted emails"
        );
    }
}