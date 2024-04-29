namespace RedditClone.Application.UserCommunities.Commands.UserCommunityRoleUpdate;

using ErrorOr;
using MediatR;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.UserCommunitiesAggregate.Enum;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.UserCommunities.Results.UserCommunityRoleUpdateResult;

public record UserCommunityRoleUpdateCommand(
    UserId RequesterId,
    CommunityId CommunityId,
    UserId UserId,
    Role Role)
: IRequest<ErrorOr<UserCommunityRoleUpdateResult>>;