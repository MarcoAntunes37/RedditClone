namespace RedditClone.Application.User.Results.UpdateProfile;

using RedditClone.Domain.UserAggregate;

public record UpdateProfileResult(
    string Message,
    User User
);