namespace RedditClone.Application.Community.Queries.GetCommunitiesById;

using MediatR;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.Community.Results.GetCommunityByIdResult;

public record GetCommunityByIdQuery(
    CommunityId CommunityId
): IRequest<GetCommunityByIdResult>;