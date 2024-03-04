namespace RedditClone.Contracts.Comment.UpdateReplyOnComment;

public record UpdateReplyOnCommentRequest(
    Guid CommentId,
    Guid UserId,
    string Content
);