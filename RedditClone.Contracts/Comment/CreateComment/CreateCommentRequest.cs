namespace RedditClone.Contracts.Comment;

public record CreateCommentRequest(
    Guid UserId,
    string Content
);