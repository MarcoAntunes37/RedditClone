namespace RedditClone.Contracts.Post.DeletePost;

public record DeletePostRequest(
    Guid UserId,
    Guid CommunityId
);
