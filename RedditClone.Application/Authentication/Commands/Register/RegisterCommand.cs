using ErrorOr;
using MediatR;
using RedditClone.Application.Authentication.Results.Register;

namespace RedditClone.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password):IRequest<ErrorOr<RegisterResult>>;