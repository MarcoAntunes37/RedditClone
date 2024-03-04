namespace RedditClone.Contracts.AddUserCommunities;

public record AddUserCommunitiesRequest(
    Guid UserId,
    Guid CommunityId
);