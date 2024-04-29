namespace RedditClone.API.Endpoints.UserCommunities.UserLeftACommunity;

public record UserLeftACommunityRequest(
    Guid UserId,
    Guid CommunityId
);