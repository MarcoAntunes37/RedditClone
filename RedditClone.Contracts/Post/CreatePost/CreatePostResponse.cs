using RedditClone.Contracts.Post.CreatePost.Models;

namespace RedditClone.Contracts.Post;

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