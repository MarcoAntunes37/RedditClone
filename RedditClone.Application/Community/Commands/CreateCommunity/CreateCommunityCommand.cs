using ErrorOr;
using MediatR;
using RedditClone.Domain.CommunityAggregate;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

namespace RedditClone.Application.Community.Commands.CreateCommunity;

public record CreateCommunityCommand(
    string Name,
    string Description,
    string Topic,
    DateTime CreatedAt,
    DateTime UpdatedAt
) : IRequest<ErrorOr<CommunityAggregate>>;