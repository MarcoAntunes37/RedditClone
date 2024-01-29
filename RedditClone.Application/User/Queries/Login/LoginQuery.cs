using ErrorOr;
using MediatR;
using RedditClone.Application.User.Results.Login;

namespace RedditClone.Application.User.Queries.Login;

public record LoginQuery(
    string Email,
    string Password):IRequest<ErrorOr<LoginResult>>;