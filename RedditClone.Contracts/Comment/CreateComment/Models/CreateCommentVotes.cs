namespace RedditClone.Contracts.Comment.CreateComment.Models;

public record CreateCommentVotes(
    string PostId,
    string UserId,
    string CommunityId
);