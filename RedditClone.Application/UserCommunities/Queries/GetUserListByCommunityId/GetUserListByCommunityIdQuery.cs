namespace RedditClone.Application.UserCommunities.Queries.GetUserListByCommunityId;

using MediatR;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.UserCommunities.Results.GetUserListByCommunityIdResults;

public record GetUserListByCommunityIdQuery(
    CommunityId CommunityId,
    int Page,
    int PageSize)
: IRequest<GetUserListByCommunityIdResult>;