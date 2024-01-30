using RedditClone.Contracts.Comment.CreateComment.Models;

namespace RedditClone.Contracts.Community;

public record CreateCommentResponse(
    string Id,
    string UserId,
    string PostId,
    string Content,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    List<CreateCommentReplies> Replies,
    List<CreateCommentUpvotes> Upvotes,
    List<CreateCommentDownvotes> Downvotes
);