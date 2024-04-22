namespace RedditClone.API.Endpoints.UserCommunities.UserCommunityRoleUpdate;

public record UserCommunityRoleUpdateRequest(
    Guid UserId,
    Guid CommunityId,
    int Role);