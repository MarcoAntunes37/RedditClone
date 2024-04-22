namespace RedditClone.API.Endpoints.PostVotes.UpdatePostVote;

public record UpdatePostVoteRequest(
    Guid UserId,
    bool IsVoted
);