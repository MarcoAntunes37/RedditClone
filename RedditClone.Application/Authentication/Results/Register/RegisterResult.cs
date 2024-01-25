using RedditClone.Domain.UserAggregate;

namespace RedditClone.Application.Authentication.Results.Register;

public record RegisterResult(
    UserAggregate User,
    string Token
);