namespace RedditClone.Contracts.Posts;

public record CreatePostRequest(
    string PostId,
    string Title,
    string Content,
    string UserId,
    string CommunityId
);
