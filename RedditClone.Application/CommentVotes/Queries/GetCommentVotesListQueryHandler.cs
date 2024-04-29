namespace RedditClone.Application.CommentVotes.Queries;

using MediatR;
using Serilog;
using RedditClone.Application.Persistence;
using RedditClone.Application.CommentVotes.Results.GetCommentVotesListResult;

public class GetCommentVotesListQueryHandler
: IRequestHandler<GetCommentVotesListQuery, GetCommentVotesListResult>
{
    private readonly ICommentRepository _commentRepository;

    public GetCommentVotesListQueryHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<GetCommentVotesListResult> Handle(
        GetCommentVotesListQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@GetCommentVotesListQuery}",
            "Trying to retrieve comment votes list",
            query);

        var votes = _commentRepository.GetVoteListByCommentId(query.CommentId);

        var result = new GetCommentVotesListResult(votes.Value);

        Log.Information(
            "{@GetCommentVotesListResult}",
            result);

        return result;
    }
}
