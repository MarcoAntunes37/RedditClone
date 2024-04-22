namespace RedditClone.Application.UserCommunities.Queries.GetUserCommunity;

using MediatR;
using Serilog;
using ErrorOr;
using RedditClone.Domain.UserCommunitiesAggregate;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.UserCommunities.Results.GetUserCommunityResults;

public class GetUserCommunityQueryHandler(
    IUserCommunitiesRepository userCommunitiesRepository)
    : IRequestHandler<GetUserCommunityQuery, ErrorOr<GetUserCommunityResult>>
{
    private readonly IUserCommunitiesRepository _userCommunitiesRepository = userCommunitiesRepository;

    public async Task<ErrorOr<GetUserCommunityResult>> Handle(
        GetUserCommunityQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@LoginQuery}",
            "Trying to retrieve userCommunities data",
            query);

        UserCommunities userCommunities = _userCommunitiesRepository.GetUserCommunities(query.UserId, query.CommunityId);

        Log.Information(
            "UserCommunities Data: {@UserCommunities}",
            userCommunities);

        var result = new GetUserCommunityResult(userCommunities);

        return result;
    }
}