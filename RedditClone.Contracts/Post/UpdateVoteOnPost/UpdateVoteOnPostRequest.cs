namespace RedditClone.Contracts.Post.UpdateVoteOnPost;
public record UpdateVoteOnPostRequest(
    Guid UserId,
    bool IsVoted
);