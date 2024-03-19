using MediatR;
using RedditClone.Application.User.Results.Register;
using RedditClone.Domain.UserAggregate.ValueObjects;

namespace RedditClone.Application.User.Commands.Register;

public record RegisterCommand(
    UserId UserId,
    string Firstname,
    string Lastname,
    string Username,
    string Password,
    string MatchPassword,
    string Email,
    DateTime CreatedAt,
    DateTime UpdatedAt
) : IRequest<RegisterResult>;