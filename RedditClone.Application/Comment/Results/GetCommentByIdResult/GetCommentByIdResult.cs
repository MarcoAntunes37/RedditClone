namespace RedditClone.Application.Comment.Results.GetCommentByIdResult;

using RedditClone.Domain.CommentAggregate;

public record GetCommentByIdResult(
    Comment Comment
);