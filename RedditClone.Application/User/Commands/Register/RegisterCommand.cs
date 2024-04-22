namespace RedditClone.Application.User.Commands.Register;

using ErrorOr;
using MediatR;
using RedditClone.Application.User.Results.Register;

public record RegisterCommand(
    string Firstname,
    string Lastname,
    string Username,
    string Password,
    string RepeatPassword,
    string Email
) : IRequest<ErrorOr<RegisterResult>>;