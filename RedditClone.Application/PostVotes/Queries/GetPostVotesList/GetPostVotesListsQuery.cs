namespace RedditClone.Application.PostVotes.Queries.GetPostVotesList;

using MediatR;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Application.PostVotes.Results.GetPostListByCommunityIdResult;

public record GetPostVotesListQuery(
    PostId PostId,
    int Page,
    int PageSize
): IRequest<GetPostVotesListResult>;