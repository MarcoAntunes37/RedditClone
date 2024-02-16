namespace RedditClone.Contracts.Community.UpdateComment;

public record UpdateCommentRequest(
    Guid UserId,
    string Content
);
