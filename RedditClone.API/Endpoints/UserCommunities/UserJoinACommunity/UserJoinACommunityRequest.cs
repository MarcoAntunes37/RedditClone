namespace RedditClone.API.Endpoints.UserCommunities.UserJoinACommunity;
public record UserJoinACommunityRequest(
    Guid CurrentUserId,
    Guid CommunityId,
    int Role);