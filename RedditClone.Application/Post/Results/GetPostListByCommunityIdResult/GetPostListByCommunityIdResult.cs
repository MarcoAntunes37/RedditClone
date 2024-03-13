namespace RedditClone.Application.Post.Results.GetPostListByCommunityIdResult;

using RedditClone.Domain.PostAggregate;

public record GetPostListByCommunityIdResult(
    List<Post> Posts,
    int Page,
    int PageSize,
    int TotalItems
);