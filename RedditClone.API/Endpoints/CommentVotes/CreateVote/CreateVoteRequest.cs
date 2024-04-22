namespace RedditClone.API.Endpoints.CommentVotes.CreateVote;

public record CreateVoteRequest(
    Guid UserId,
    bool IsVoted
);