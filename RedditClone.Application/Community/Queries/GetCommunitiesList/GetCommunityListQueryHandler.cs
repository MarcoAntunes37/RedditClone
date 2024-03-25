namespace RedditClone.Application.Comment.Queries.GetCommunitiesList;

using RedditClone.Application.Community.Results.GetCommunitiesListResult;
using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Application.Community.Queries.GetCommunitiesList;
using RedditClone.Domain.CommunityAggregate;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Common.Helpers;
using Serilog;

public class GetCommunitiesListQueryHandler
: IRequestHandler<GetCommunitiesListQuery, GetCommunitiesListResult>
{
    private readonly ICommunityRepository _communityRepository;
    private readonly IConfiguration _configuration;

    public GetCommunitiesListQueryHandler(
        ICommunityRepository communityRepository,
        IConfiguration configuration)
    {
        _communityRepository = communityRepository;
        _configuration = configuration;
    }

    public async Task<GetCommunitiesListResult> Handle(GetCommunitiesListQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@GetCommunitiesListQuery}",
            "Trying to retrieve list of communities");

        List<Community> communities = _communityRepository.GetCommunitiesList();

        if (!string.IsNullOrEmpty(query.Name))
        {
            communities = communities.Where(
                c => c.Name.Contains(query.Name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        if (!string.IsNullOrEmpty(query.Topic))
        {
            communities = communities.Where(
                c => c.Topic.Contains(query.Topic, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        int totalItems = communities.Count;
        int startIndex = (query.Page -1) * query.PageSize;
        int endIndex = Math.Min(startIndex + query.PageSize -1, totalItems -1);

        List<Community> pageCommunities = communities.GetRange(startIndex, endIndex - startIndex + 1);

        GetCommunitiesListResult result = new(pageCommunities, query.Page, query.PageSize, totalItems);

        Log.Information(
            "{@GetCommunitiesListResult}",
            result);

        return result;
    }
}