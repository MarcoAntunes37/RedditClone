namespace RedditClone.Application.Community.Results.GetCommunitiesListResult;

using RedditClone.Domain.CommunityAggregate;

public record GetCommunitiesListResult(
    List<Community> Communities,
    int Page,
    int PageSize,
    int TotalItems
);