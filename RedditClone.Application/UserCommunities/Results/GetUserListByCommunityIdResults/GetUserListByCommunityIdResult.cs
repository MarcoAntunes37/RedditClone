namespace RedditClone.Application.UserCommunities.Results.GetUserListByCommunityIdResults;

using Domain.UserAggregate;

public record GetUserListByCommunityIdResult(
    IList<User> Users,
    int TotalPages,
    int TotalItems,
    int Page,
    int PageSize);