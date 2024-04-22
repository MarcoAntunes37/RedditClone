namespace RedditClone.Application.UserCommunities.Results.GetUserCommunityResults;

using Domain.UserCommunitiesAggregate;
public record GetUserCommunityResult(
    UserCommunities UserCommunities
);