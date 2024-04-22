namespace RedditClone.Application.Community.Commands.DeleteCommunity;

using ErrorOr;
using MediatR;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.UserCommunitiesAggregate.Enum;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.Community.Results.DeleteCommunityResult;

public record DeleteCommunityCommand(
    CommunityId CommunityId,
    UserId UserId,
    Role Role
) : IRequest<ErrorOr<DeleteCommunityResult>>;