namespace RedditClone.Contracts.Post.CreatePost;

using RedditClone.Contracts.Post.CreatePost.Models;

public record CreatePostResponse(
    string Id,
    string CommunityId,
    string UserId,
    string Title,
    string Content,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    List<CreatePostVotes> Votes
);