namespace RedditClone.Contracts.Community.UpdateCommunity;

public record UpdateCommunityRequest(
    Guid UserId,
    string Name,
    string Description,
    string Topic
);
