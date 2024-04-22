namespace RedditClone.API.Endpoints.Community.UpdateCommunity;

public record UpdateCommunityResponse(
    string Message,
    string Name,
    string Description,
    string Topic,
    DateTime CreatedAt,
    DateTime UpdatedAt
);