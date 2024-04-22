namespace RedditClone.API.Endpoints.CommentVotes.UpdateVote;

public record UpdateVoteRequest(
    Guid UserId,
    bool IsVoted
);