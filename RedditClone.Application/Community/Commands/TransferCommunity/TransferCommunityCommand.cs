namespace RedditClone.Application.Community.Commands.TransferCommunity;

using MediatR;
using RedditClone.Application.Community.Results.TransferCommunityResult;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

public record TransferCommunityCommand(
    UserId UserId,
    UserId NewUserId,
    CommunityId CommunityId
) : IRequest<TransferCommunityResult>;