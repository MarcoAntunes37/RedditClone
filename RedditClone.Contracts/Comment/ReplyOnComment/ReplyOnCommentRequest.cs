namespace RedditClone.Contracts.Comment.ReplyOnComment;

public record ReplyOnCommentRequest(
    Guid CommentId,
    Guid UserId,
    string Content
);