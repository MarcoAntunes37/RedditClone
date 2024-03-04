namespace RedditClone.Contracts.Comment.UpdateVoteOnReply;
public record UpdateVoteOnReplyRequest(
    Guid UserId,
    bool IsVoted
);