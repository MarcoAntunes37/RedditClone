namespace RedditClone.Contracts.Comment.ReplyOnComment;

public record ReplyOnCommentRequest(
    Guid UserId,
    Guid CommunityId,
    string Content
);