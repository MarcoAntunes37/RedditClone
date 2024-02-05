namespace RedditClone.Contracts.Community;

public record CreateCommentResponse(
    string Id,
    string Content,
    DateTime CreatedAt,
    DateTime UpdatedAt
);