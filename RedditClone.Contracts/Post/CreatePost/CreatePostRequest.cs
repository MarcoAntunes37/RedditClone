namespace RedditClone.Contracts.Post.CreatePost;

public record CreatePostRequest(
    Guid UserId,
    Guid CommunityId,
    string Title,
    string Content
);
