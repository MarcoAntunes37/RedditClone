namespace RedditClone.Application.Community.Commands.CreateCommunity;

using MediatR;
using RedditClone.Application.Community.Results.CreateCommunityResult;
using RedditClone.Domain.UserAggregate.ValueObjects;

public record CreateCommunityCommand(
    string Name,
    string Description,
    string Topic,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    UserId UserId
) : IRequest<CreateCommunityResult>;