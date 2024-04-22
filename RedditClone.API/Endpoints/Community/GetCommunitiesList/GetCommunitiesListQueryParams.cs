namespace RedditClone.API.Endpoints.Community.GetCommunitiesList;

public record GetCommunitiesListQueryParams(
    string? Name = null,
    string? Topic = null,
    int Page = 1,
    int PageSize = 20
);