using RedditClone.Domain.CommunityAggregate;

namespace RedditClone.Application.Community.Results.CreateCommunityResult;

public record CreateCommunityResult(
    CommunityAggregate Community
);