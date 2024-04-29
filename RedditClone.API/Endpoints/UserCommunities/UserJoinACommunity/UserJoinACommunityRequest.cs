namespace RedditClone.API.Endpoints.UserCommunities.UserJoinACommunity;
public record UserJoinACommunityRequest(
    Guid UserId,
    Guid CommunityId);