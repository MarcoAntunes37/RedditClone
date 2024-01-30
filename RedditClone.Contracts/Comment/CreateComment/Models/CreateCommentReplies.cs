namespace RedditClone.Contracts.Comment.CreateComment.Models;

public record CreateCommentReplies(
    string UserId,
    string Content,
    List<CreateCommentUpvotes> Upvotes,
    List<CreateCommentDownvotes> Downvotes
);