namespace RedditClone.API.Endpoints.Community.CreateCommunity;

public record CreateCommunityResponse(
    Guid Id,
    string Name,
    string Description,
    string Topic,
    DateTime CreatedAt,
    DateTime UpdatedAt);