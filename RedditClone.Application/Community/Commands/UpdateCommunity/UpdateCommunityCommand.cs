namespace RedditClone.Application.Community.Commands.UpdateCommunity;

using ErrorOr;
using MediatR;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.Community.Results.UpdateCommunityResult;

public record UpdateCommunityCommand(
    CommunityId CommunityId,
    UserId UserId,
    string Name,
    string Description,
    string Topic
) : IRequest<ErrorOr<UpdateCommunityResult>>;