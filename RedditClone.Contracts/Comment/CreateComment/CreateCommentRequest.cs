namespace RedditClone.Contracts.Comment.CreateComment;

public record CreateCommentRequest(
    Guid UserId,
    string Content
);