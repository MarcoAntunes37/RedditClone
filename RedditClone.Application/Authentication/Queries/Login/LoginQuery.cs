using ErrorOr;
using MediatR;
using RedditClone.Application.Authentication.Results.Login;

namespace RedditClone.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password):IRequest<ErrorOr<LoginResult>>;