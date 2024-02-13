namespace RedditClone.Contracts.Post.CreatePost.Models;

public record CreatePostVotes(
    string PostId,
    string UserId,
    string CommunityId
);