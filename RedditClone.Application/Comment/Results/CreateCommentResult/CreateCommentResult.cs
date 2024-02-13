namespace RedditClone.Application.Comment.Results.CreateCommentResult;

using RedditClone.Domain.CommentAggregate;

public record CreateCommentResult(
    Comment Comment
);