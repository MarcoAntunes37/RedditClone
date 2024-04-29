using MediatR;
using RedditClone.Application.UserCommunities.Results.GetCommunitiesListByUserIdResults;
using RedditClone.Domain.UserAggregate.ValueObjects;

namespace RedditClone.Application.UserCommunities.Queries.GetCommunitiesListByUserId;

public record GetCommunitiesListByUserIdQuery(
    UserId UserId,
    int Page,
    int PageSize
)
: IRequest<GetCommunitiesListByUserIdResult>;