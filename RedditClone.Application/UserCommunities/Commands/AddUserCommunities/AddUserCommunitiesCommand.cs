namespace RedditClone.Application.UserCommunities.Commands.AddUserCommunities;

using MediatR;
using RedditClone.Application.UserCommunities.Results.AddUserCommunitiesResults;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public record AddUserCommunitiesCommand(
    CommunityId CommunityId,
    UserId UserId)
: IRequest<AddUserCommunitiesResult>;