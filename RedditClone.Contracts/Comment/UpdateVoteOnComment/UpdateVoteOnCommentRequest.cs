namespace RedditClone.Contracts.Post.UpdateVoteOnComment;
public record UpdateVoteOnCommentRequest(
    Guid UserId,
    string Content
);