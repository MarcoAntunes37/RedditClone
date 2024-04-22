namespace RedditClone.Application.User.Commands.SendPasswordRecoveryEmail;

using ErrorOr;
using MediatR;
using RedditClone.Application.User.Results.SendPasswordRecoveryEmail;

public record SendPasswordRecoveryEmailCommand(
    string Email
) : IRequest<ErrorOr<SendPasswordRecoveryEmailResult>>;