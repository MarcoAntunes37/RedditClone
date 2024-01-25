using ErrorOr;
using MediatR;
using RedditClone.Application.Authentication.Results.Register;
using RedditClone.Domain.UserAggregate.ValueObjects;

namespace RedditClone.Application.Authentication.Commands.Register;

public record RegisterCommand(
    UserId UserId,
    string FirstName,
    string LastName,
    string Username,
    string Password,
    string Email,
    DateTime CreatedAt,
    DateTime UpdatedAt):IRequest<ErrorOr<RegisterResult>>;