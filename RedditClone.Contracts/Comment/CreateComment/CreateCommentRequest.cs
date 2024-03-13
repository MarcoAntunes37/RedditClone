namespace RedditClone.Contracts.Comment.CreateComment;

public record CreateCommentRequest(
    Guid UserId,
    Guid CommunityId,
    string Content
);