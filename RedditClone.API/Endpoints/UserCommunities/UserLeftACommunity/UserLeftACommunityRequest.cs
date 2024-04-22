namespace RedditClone.API.Endpoints.UserCommunities.UserLeftACommunity;

public record UserLeftACommunityRequest(
    Guid CurrentUserId,
    Guid CommunityId,
    int Role
);