namespace RedditClone.API.Endpoints.Post.CreatePost;

public record CreatePostResponse(
    Guid UserId,
    Guid CommunityId,
    string Title,
    string Content,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    string Message);
