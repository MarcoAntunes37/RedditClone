namespace RedditClone.Application.UserCommunities.Commands.RemoveUserCommunities;

using MediatR;
using RedditClone.Application.UserCommunities.Results.AddUserCommunitiesResults;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public record RemoveUserCommunitiesCommand(
    CommunityId CommunityId,
    UserId UserId)
: IRequest<RemoveUserCommunitiesResult>;