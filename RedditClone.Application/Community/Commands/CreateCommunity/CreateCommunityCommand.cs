using MediatR;
using RedditClone.Application.Community.Results.CreateCommunityResult;

namespace RedditClone.Application.Community.Commands.CreateCommunity;

public record CreateCommunityCommand(
    string Name,
    string Description,
    string Topic,
    DateTime CreatedAt,
    DateTime UpdatedAt
) : IRequest<CreateCommunityResult>;