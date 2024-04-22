namespace RedditClone.Application.Comment.Results.UpdateCommentResult;

using RedditClone.Domain.CommentAggregate;

public record UpdateCommentResult(
    string Message,
    Comment Comment
);