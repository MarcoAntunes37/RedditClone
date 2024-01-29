using RedditClone.Contracts.Posts.CreatePost.Models;

namespace RedditClone.Contracts.Posts;

public record CreatePostResponse(
    string PostId,
    string Title,
    string Content,
    string UserId,
    string CommunityId,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    List<CreatePostUpvotes> Upvotes,
    List<CreatePostDownvotes> Downvotes
);