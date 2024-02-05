namespace RedditClone.Contracts.Community;

public record CreateCommunityRequest(
    string Name,
    string Description,
    string Topic
);
