using RedditClone.Domain.CommentAggregate;

namespace RedditClone.Application.Comment.Results.CreateCommentResult;

public record CreateCommentResult(
    CommentAggregate Comment
);