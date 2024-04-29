namespace RedditClone.Application.UserCommunities.Queries.GetUserListByCommunityId;

using MediatR;
using Serilog;
using ErrorOr;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.UserCommunities.Results.GetUserListByCommunityIdResults;
using RedditClone.Application.Common.Extensions;

public class GetUserListByCommunityIdQueryHandler(
    IUserCommunitiesRepository userCommunitiesRepository)
    : IRequestHandler<GetUserListByCommunityIdQuery, GetUserListByCommunityIdResult>
{
    private readonly IUserCommunitiesRepository _userCommunitiesRepository = userCommunitiesRepository;

    public async Task<GetUserListByCommunityIdResult> Handle(
        GetUserListByCommunityIdQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@LoginQuery}",
            "Trying to retrieve userCommunities data",
            query);

        var userCommunities = _userCommunitiesRepository.GetUsersListByCommunityId(query.CommunityId);

        var totalItems = userCommunities.Count;

        var pagedUserCommunities = PaginationHandler.ApplyPagination(
            userCommunities, query.Page, query.PageSize);

        var result = new GetUserListByCommunityIdResult(
            pagedUserCommunities.Item1, pagedUserCommunities.Item2, totalItems, query.Page, query.PageSize);

        return result;
    }
}