namespace RedditClone.Contracts.Post.VoteOnPost;
public record VoteOnPostRequest(
    Guid UserId,
    bool IsVoted
);