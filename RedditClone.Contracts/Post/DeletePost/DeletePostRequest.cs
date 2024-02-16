namespace RedditClone.Contracts.Community.DeletePost;

public record DeletePostRequest(
    Guid UserId,
    Guid CommunityId
);
