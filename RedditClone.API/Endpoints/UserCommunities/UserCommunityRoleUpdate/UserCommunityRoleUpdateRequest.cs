namespace RedditClone.API.Endpoints.UserCommunities.UserCommunityRoleUpdate;

public record UserCommunityRoleUpdateRequest(
    Guid RequesterId,
    Guid UserId,
    Guid CommunityId,
    int Role);