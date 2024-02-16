namespace RedditClone.Contracts.Post;

public record CreatePostRequest(
    Guid UserId,
    Guid CommunityId,
    string Title,
    string Content
);
