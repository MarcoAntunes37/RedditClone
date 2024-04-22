namespace RedditClone.API.Endpoints.Post.CreatePost;

public record CreatePostRequest(
    Guid UserId,
    Guid CommunityId,
    string Title,
    string Content
);
