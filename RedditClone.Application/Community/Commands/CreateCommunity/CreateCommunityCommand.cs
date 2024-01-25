using ErrorOr;
using MediatR;
using RedditClone.Domain.CommunityAggregate;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

namespace RedditClone.Application.Community.Commands.CreateCommunity;

public record CreateCommunityCommand(
    UserId UserId,
    string Name,
    string Description,
    int MembersCount,
    string Topic,
    DateTime CreatedAt,
    DateTime UpdatedAt
) : IRequest<ErrorOr<CommunityAggregate>>;