namespace RedditClone.Application.UserCommunities.Queries.GetUserCommunity;

using ErrorOr;
using MediatR;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.UserCommunities.Results.GetUserCommunityResults;

public record GetUserCommunityQuery(
    UserId UserId,
    CommunityId CommunityId)
: IRequest<ErrorOr<GetUserCommunityResult>>;