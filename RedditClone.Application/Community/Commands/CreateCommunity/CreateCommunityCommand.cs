using ErrorOr;
using MediatR;
using RedditClone.Domain.CommunityAggregate;

namespace RedditClone.Application.Community.Commands.CreateCommunity;

public record CreateCommunityCommand(
    string Name,
    string Description,
    int MembersCount,
    string Topic
) : IRequest<ErrorOr<CommunityAggregate>>;