namespace RedditClone.Application.Community.Commands.DeleteCommunity;

using MediatR;
using RedditClone.Application.Community.Results.DeleteCommunityResult;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public record DeleteCommunityCommand(
    CommunityId CommunityId,
    UserId UserId
) : IRequest<DeleteCommunityResult>;