namespace RedditClone.Contracts.Comment.VoteOnComment;
public record VoteOnCommentRequest(
    Guid UserId,
    bool IsVoted
);