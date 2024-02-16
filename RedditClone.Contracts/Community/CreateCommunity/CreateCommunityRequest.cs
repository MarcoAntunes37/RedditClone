namespace RedditClone.Contracts.Community.CreateCommunity;

public record CreateCommunityRequest(
    string Name,
    string Description,
    string Topic
);
