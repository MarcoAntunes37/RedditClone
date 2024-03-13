namespace RedditClone.Application.Community.Results.GetCommunityByIdResult;

using RedditClone.Domain.CommunityAggregate;
public record GetCommunityByIdResult(
    Community Community
);