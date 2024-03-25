namespace RedditClone.Application.Community.Results.UpdateCommentResult;

using RedditClone.Domain.CommentAggregate;

public record UpdateCommentResult(
    string Message,
    Comment Comment
);