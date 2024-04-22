namespace RedditClone.API.Endpoints.Community.UpdateCommunity;

public record UpdateCommunityRequest(
    Guid UserId,
    string Name,
    string Description,
    string Topic);