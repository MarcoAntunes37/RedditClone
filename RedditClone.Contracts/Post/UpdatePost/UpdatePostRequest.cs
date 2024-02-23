namespace RedditClone.Contracts.Post.UpdatePost;

public record UpdatePostRequest(
    Guid PostId,
    Guid UserId,
    string Title,
    string Content
);