using RedditClone.Domain.UserAggregate.ValueObjects;

namespace RedditClone.API.Endpoints.User.Delete;

public record DeleteRequest(
    UserId CurrentUserId
);