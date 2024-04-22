namespace RedditClone.Application.UserCommunities.Commands.UserLeftACommunity;

using ErrorOr;
using MediatR;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.UserCommunities.Results.UserJoinACommunityResults;

public record UserLeftACommunityCommand(
    CommunityId CommunityId,
    UserId UserId)
: IRequest<ErrorOr<UserLeftACommunityResult>>;