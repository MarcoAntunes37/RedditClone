namespace RedditClone.API.Endpoints.ReplyVotes.CreateReplyVote;

public record CreateReplyVoteRequest(
    Guid UserId,
    bool IsVoted
);