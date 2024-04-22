namespace RedditClone.Application.User.Results.Register;

using RedditClone.Domain.UserAggregate;

public record RegisterResult(
    User User,
    string Token);