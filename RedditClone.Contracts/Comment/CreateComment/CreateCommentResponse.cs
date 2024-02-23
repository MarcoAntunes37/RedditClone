namespace RedditClone.Contracts.Comment.CreateComment;

using RedditClone.Contracts.Comment.CreateComment.Models;

public record CreateCommentResponse(
    string Id,
    string UserId,
    string PostId,
    string Content,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    List<CreateCommentVotes> Votes,
    List<CreateCommentReplies> Replies
);