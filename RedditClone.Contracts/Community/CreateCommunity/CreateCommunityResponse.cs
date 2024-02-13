namespace RedditClone.Contracts.Community;

public record CreateCommunityResponse(
    string Id,
    string Name,
    string Description,
    string Topic,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    string UserId
);