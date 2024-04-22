namespace RedditClone.Application.Community.Commands.CreateCommunity;

using ErrorOr;
using MediatR;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Application.Community.Results.CreateCommunityResult;

public record CreateCommunityCommand(
    string Name,
    string Description,
    string Topic,
    UserId UserId
) : IRequest<ErrorOr<CreateCommunityResult>>;