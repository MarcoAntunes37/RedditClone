namespace RedditClone.Application.PostVotes.Results.GetPostListByCommunityIdResult;

using RedditClone.Domain.PostAggregate.Entities;
public record GetPostVotesListResult(
    List<Votes> Votes,
    int Page,
    int PageSize,
    int TotalItems
);