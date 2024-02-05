namespace RedditClone.Contracts.Post;

public record CreatePostResponse(
    string Id,
    string Title,
    string Content,
    DateTime CreatedAt,
    DateTime UpdatedAt
);