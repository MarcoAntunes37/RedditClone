namespace RedditClone.Application.UserCommunities.Commands.UserJoinACommunity;

using ErrorOr;
using MediatR;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.UserCommunities.Results.UserJoinACommunityResults;

public record UserJoinACommunityCommand(
    CommunityId CommunityId,
    UserId UserId)
: IRequest<ErrorOr<UserJoinACommunityResult>>;