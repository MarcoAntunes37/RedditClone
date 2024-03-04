namespace RedditClone.Contracts.Comment.VoteOnReply;
public record VoteOnReplyRequest(
    Guid UserId,
    bool IsVoted
);