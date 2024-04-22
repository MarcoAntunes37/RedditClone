namespace RedditClone.API.Endpoints.Community.CreateCommunity;

public record CreateCommunityRequest(
    Guid OwnerId,
    string Name,
    string Description,
    string Topic
);