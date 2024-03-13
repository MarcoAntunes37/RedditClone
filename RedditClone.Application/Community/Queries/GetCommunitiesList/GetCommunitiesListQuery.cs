namespace RedditClone.Application.Community.Queries.GetCommunitiesList;

using MediatR;
using RedditClone.Application.Community.Results.GetCommunitiesListResult;

public record GetCommunitiesListQuery(
    string Name,
    string Topic,
    int Page,
    int PageSize
): IRequest<GetCommunitiesListResult>;