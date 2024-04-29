namespace RedditClone.Application.Community.Results.GetCommunitiesListResult;

using RedditClone.Domain.CommunityAggregate;

public record GetCommunitiesListResult(
    IList<Community> Communities,
    int TotalPages,
    int Page,
    int PageSize,
    int TotalItems
);