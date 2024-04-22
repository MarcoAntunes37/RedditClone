namespace RedditClone.API.Endpoints.Comment.UpdateComment;

public record UpdateCommentRequest(
    Guid UserId,
    string Content
);