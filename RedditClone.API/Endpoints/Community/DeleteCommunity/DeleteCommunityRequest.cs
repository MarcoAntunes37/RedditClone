namespace RedditClone.API.Endpoints.Community.DeleteCommunity;

using RedditClone.Domain.UserCommunitiesAggregate.Enum;

public record DeleteCommunityRequest(
    Guid UserId,
    int Role);