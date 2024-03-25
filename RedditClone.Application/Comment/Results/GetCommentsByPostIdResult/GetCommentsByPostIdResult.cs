namespace RedditClone.Application.Comment.Results.GetCommentsByPostIdResult;

using RedditClone.Domain.CommentAggregate;

public record GetCommentsByPostIdResult(
    List<Comment> Comments
);