namespace RedditClone.Application.User.Queries.Login;

using ErrorOr;
using MediatR;
using RedditClone.Application.User.Results.Login;

public record LoginQuery(
    string Email,
    string Password)
: IRequest<ErrorOr<LoginResult>>;