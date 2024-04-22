namespace RedditClone.Application.User.Results.UpdateProfile;

using RedditClone.Domain.UserAggregate;

public record UpdateProfileResult(
    User User
);