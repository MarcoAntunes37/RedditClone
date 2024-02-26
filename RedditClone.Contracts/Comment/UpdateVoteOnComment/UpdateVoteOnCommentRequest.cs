namespace RedditClone.Contracts.Post.UpdateVoteOnComment;
public record UpdateVoteOnCommentRequest(
    Guid UserId,
    bool IsVoted
);