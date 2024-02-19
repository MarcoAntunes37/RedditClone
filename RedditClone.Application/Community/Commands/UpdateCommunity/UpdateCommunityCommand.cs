namespace RedditClone.Application.Community.Commands.UpdateCommunity;

using MediatR;
using RedditClone.Application.Community.Results.UpdateCommunityResult;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

public record UpdateCommunityCommand(
    CommunityId CommunityId,
    UserId UserId,
    string Name,
    string Description,
    string Topic
) : IRequest<UpdateCommunityResult>;