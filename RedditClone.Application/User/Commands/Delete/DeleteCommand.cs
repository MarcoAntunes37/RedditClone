namespace RedditClone.Application.User.Commands.Delete;

using MediatR;
using RedditClone.Application.User.Results;
using RedditClone.Domain.UserAggregate.ValueObjects;

public record DeleteUserCommand(
    UserId UserId
) : IRequest<DeleteResult>;