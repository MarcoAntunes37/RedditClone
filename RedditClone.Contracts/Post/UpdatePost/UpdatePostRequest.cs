namespace RedditClone.Contracts.Community.UpdatePost;

public record UpdatePostRequest(
    Guid PostId,
    Guid UserId,
    string Title,
    string Content
);