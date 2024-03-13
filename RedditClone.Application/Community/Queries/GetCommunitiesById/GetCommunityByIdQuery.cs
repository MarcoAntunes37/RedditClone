namespace RedditClone.Application.Community.Queries.GetCommunitiesById;

using MediatR;
using RedditClone.Application.Community.Results.GetCommunityByIdResult;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

public record GetCommunityByIdQuery(
    CommunityId CommunityId
): IRequest<GetCommunityByIdResult>;