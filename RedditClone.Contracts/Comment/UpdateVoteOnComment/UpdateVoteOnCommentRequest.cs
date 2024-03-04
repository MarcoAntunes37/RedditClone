namespace RedditClone.Contracts.Comment.UpdateVoteOnComment;
public record UpdateVoteOnCommentRequest(
    Guid UserId,
    bool IsVoted
);