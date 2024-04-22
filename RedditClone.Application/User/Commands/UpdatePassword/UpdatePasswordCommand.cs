namespace RedditClone.Application.User.Commands.UpdatePassword;

using MediatR;
using ErrorOr;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Application.User.Results.UpdatePassword;

public record UpdatePasswordCommand(
    UserId UserId,
    string OldPassword,
    string NewPassword,
    string MatchPassword
) : IRequest<ErrorOr<UpdatePasswordResult>>;