namespace RedditClone.API.Endpoints.ReplyVotes.UpdateUpdateReplyVoteVote;

public record UpdateReplyVoteRequest(
    Guid UserId,
    bool IsVoted
);