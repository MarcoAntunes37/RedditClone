namespace RedditClone.Contracts.Comment.CreateComment.Models;

public record CreateCommentReplies(
    string UserId,
    string Username,
    string Content,
    List<CreateCommentVotes> Votes
);