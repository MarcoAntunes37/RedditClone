namespace RedditClone.Contracts.Community.CreateCommunity;

public record CreateCommunityResponse(
    string Name,
    string Description,
    string Topic,
    DateTime CreatedAt,
    DateTime UpdatedAt
);