namespace RedditClone.Application.User.Commands.SendPasswordRecoveryEmail;

using MediatR;
using RedditClone.Application.User.Results.SendPasswordRecoveryEmail;

public record SendPasswordRecoveryEmailCommand(
    string Email
) : IRequest<SendPasswordRecoveryEmailResult>;