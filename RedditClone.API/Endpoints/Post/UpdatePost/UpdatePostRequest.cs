namespace RedditClone.API.Endpoints.Post.UpdatePost;

public record UpdatePostRequest(
    Guid UserId,
    string Title,
    string Content
);