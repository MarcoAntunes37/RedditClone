namespace RedditClone.Application.Community.Commands.TransferCommunity;

using MediatR;
using RedditClone.Application.Community.Results.TransferCommunityResult;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public record TransferCommunityCommand(
    UserId UserId,
    UserId NewUserId,
    CommunityId CommunityId
) : IRequest<TransferCommunityResult>;