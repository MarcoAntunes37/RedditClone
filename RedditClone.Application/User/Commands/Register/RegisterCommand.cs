using MediatR;
using RedditClone.Application.User.Results.Register;
using RedditClone.Domain.UserAggregate.ValueObjects;

namespace RedditClone.Application.User.Commands.Register;

public record RegisterCommand(
    UserId UserId,
    string FirstName,
    string LastName,
    string Username,
    string Password,
    string Email,
    DateTime CreatedAt,
    DateTime UpdatedAt
) : IRequest<RegisterResult>;