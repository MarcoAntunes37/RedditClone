using RedditClone.Contracts.Post.CreatePost.Models;

namespace RedditClone.Contracts.Post;

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