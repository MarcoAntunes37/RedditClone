namespace RedditClone.Application.Post.Queries.GetPostVotesList;

using MediatR;
using Serilog;
using RedditClone.Domain.PostAggregate.Entities;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.PostVotes.Queries.GetPostVotesList;
using RedditClone.Application.PostVotes.Results.GetPostListByCommunityIdResult;

public class GetPostVotesListsQueryHandler(
    IPostRepository postRepository)
    : IRequestHandler<GetPostVotesListQuery, GetPostVotesListResult>
{
    private readonly IPostRepository _postRepository = postRepository;

    public async Task<GetPostVotesListResult> Handle(
        GetPostVotesListQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@GetPostListByCommunityIdQuery}",
            "Trying to retrieve list of votes of Post: {@PostId}",
            query.PostId);

        List<Votes> votes = _postRepository.GetVotesListsByPostId(query.PostId);

        GetPostVotesListResult result = new(votes, query.Page, query.PageSize, votes.Count);

        Log.Information(
            "{@GetPostVotesListResult}, {@PostId}",
            query,
            query.PostId);

        return result;
    }
}