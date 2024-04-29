namespace RedditClone.Application.Post.Results.GetPostListByCommunityIdResult;

using RedditClone.Domain.PostAggregate;

public record GetPostListByCommunityIdResult(
    IList<Post> Posts,
    int TotalPages,
    int Page,
    int PageSize,
    int TotalItems
);