namespace RedditClone.Application.User.Commands.UpdatePassword;

using MediatR;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Application.User.Results.UpdatePassword;

public record UpdatePasswordCommand(
    UserId UserId,
    string OldPassword,
    string NewPassword,
    string MatchPassword

) : IRequest<UpdatePasswordResult>;