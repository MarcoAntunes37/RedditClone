namespace RedditClone.Contracts.Community;

public record CreateCommunityResponse(
    string Id,
    string OwnerId,
    string Name,
    string Description,
    int MembersCount,
    string Topic,
    DateTime CreatedAt,
    DateTime UpdatedAt
);