using RedditClone.Domain.CommentAggregate.Entities;

namespace RedditClone.Application.CommentReplies.Results.GetCommentRepliesListResult;

public record GetCommentRepliesListResult(
    IList<Replies> Replies,
    int TotalItems,
    int TotalPages,
    int Page,
    int PageSize);