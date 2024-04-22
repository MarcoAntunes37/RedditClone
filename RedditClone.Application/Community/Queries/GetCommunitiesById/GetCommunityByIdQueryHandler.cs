namespace RedditClone.Application.Comment.Queries.GetCommunityById;

using MediatR;
using Serilog;
using RedditClone.Application.Persistence;
using RedditClone.Domain.CommunityAggregate;
using RedditClone.Application.Community.Queries.GetCommunitiesById;
using RedditClone.Application.Community.Results.GetCommunityByIdResult;

public class GetCommunityByIdQueryHandler(
    ICommunityRepository communityRepository)
: IRequestHandler<GetCommunityByIdQuery, GetCommunityByIdResult>
{
    private readonly ICommunityRepository _communityRepository = communityRepository;

    public async Task<GetCommunityByIdResult> Handle(
        GetCommunityByIdQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@GetCommunityByIdQuery}",
            "Trying to retrieve community data",
            query);

        Community community = _communityRepository.GetCommunityById(query.CommunityId).Value;

        GetCommunityByIdResult result = new(community);

        Log.Information(
            "{@GetCommunityByIdResult}",
            result);

        return result;
    }
}