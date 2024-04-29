namespace RedditClone.Application.UserCommunities.Queries.GetCommunitiesListByUserId;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RedditClone.Application.Common.Extensions;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.UserCommunities.Results.GetCommunitiesListByUserIdResults;
using Serilog;

public class GetCommunitiesListByUserIdQueryHandler
: IRequestHandler<GetCommunitiesListByUserIdQuery, GetCommunitiesListByUserIdResult>
{
    private readonly IUserCommunitiesRepository _userCommunitiesRepository;

    public GetCommunitiesListByUserIdQueryHandler(IUserCommunitiesRepository userCommunitiesRepository)
    {
        _userCommunitiesRepository = userCommunitiesRepository;
    }

    public async Task<GetCommunitiesListByUserIdResult> Handle(
        GetCommunitiesListByUserIdQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information("{@Message}",
            "Trying to retrieve list of communities of User: {@userId}",
            query.UserId);

        var userCommunities = _userCommunitiesRepository.GetCommunitiesListByUserId(query.UserId);

        var totalItems = userCommunities.Count;

        var pagedUserCommunities = PaginationHandler.ApplyPagination(userCommunities, query.Page, query.PageSize);

        var result = new GetCommunitiesListByUserIdResult(
            pagedUserCommunities.Item1, pagedUserCommunities.Item2, query.Page, query.PageSize, totalItems);

        return result;
    }
}
