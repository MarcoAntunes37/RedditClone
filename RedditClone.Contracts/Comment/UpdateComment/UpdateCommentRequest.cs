namespace RedditClone.Contracts.Comment.UpdateComment;

public record UpdateCommentRequest(
    Guid UserId,
    string Content
);
