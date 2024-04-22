namespace RedditClone.Application.User.Commands.PasswordRecoveryCodeValidate;

using ErrorOr;
using MediatR;
using RedditClone.Application.User.Results.PasswordRecoveryCodeValidate;

public record PasswordRecoveryCodeValidateCommand(
    string Code,
    string Email
) : IRequest<ErrorOr<PasswordRecoveryCodeValidateResult>>;