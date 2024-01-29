using RedditClone.Domain.UserAggregate;

namespace RedditClone.Application.User.Results.Register;

public record RegisterResult(
    UserAggregate User,
    string Token
);