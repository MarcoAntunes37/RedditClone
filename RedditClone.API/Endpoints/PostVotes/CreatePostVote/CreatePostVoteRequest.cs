namespace RedditClone.API.Endpoints.PostVotes.CreatePostVote;

public record CreatePostVoteRequest(
    Guid UserId,
    bool IsVoted
);