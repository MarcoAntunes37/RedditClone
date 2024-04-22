namespace RedditClone.API.Endpoints.ReplyVotes.DeleteReplyVote;

public record DeleteReplyVoteRequest(
    Guid VoteId,
    Guid UserId
);