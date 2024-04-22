namespace RedditClone.API.Endpoints.Post.UpdatePost;

public record UpdatePostResponse(
    Guid UserId,
    Guid CommunityId,
    string Title,
    string Content,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    string Message
);