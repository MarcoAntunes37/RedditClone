namespace RedditClone.Contracts.Community.UpdateCommunity;

public record UpdateCommunityResponse(
    string Message,
    string Name,
    string Topic,
    string Description
);