namespace RedditClone.Application.Community.Queries.GetPostListByCommunityId;

using MediatR;
using RedditClone.Application.Post.Results.GetPostListByCommunityIdResult;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

public record GetPostListByCommunityIdQuery(
    CommunityId CommunityId,
    int Page,
    int PageSize
): IRequest<GetPostListByCommunityIdResult>;