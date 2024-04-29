namespace RedditClone.Application.UserCommunities.Results.GetCommunitiesListByUserIdResults;

using Domain.CommunityAggregate;

public record GetCommunitiesListByUserIdResult(
    IList<Community> Communities,
    int TotalPages,
    int Page,
    int PageSize,
    int TotalItems
);