namespace RedditClone.Application.Comment.Results.GetCommentsListByPostIdResults;

using RedditClone.Domain.CommentAggregate;

public record GetCommentsListByPostIdResult(
    IList<Comment> Comments,
    int TotalPages,
    int TotalItems,
    int Page,
    int PageSize
);