namespace RedditClone.Application.ReplyVotes.Queries.GetReplyVotesList;

using MediatR;
using Serilog;
using RedditClone.Application.Persistence;
using RedditClone.Application.ReplyVotes.Queries.GetRepliesVotesList;
using RedditClone.Application.ReplyVotes.Results.GetReplyVotesListResults;

public class GetReplyVotesListQueryHandler
: IRequestHandler<GetReplyVotesListQuery, GetReplyVotesListResult>
{
    private readonly ICommentRepository _commentRepository;

    public GetReplyVotesListQueryHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<GetReplyVotesListResult> Handle(
        GetReplyVotesListQuery request,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@GetReplyVotesListQuery}",
            "Trying to retrieve list of votes on Reply: {@ReplyId}",
            request,
            request.ReplyId);

        var votes = _commentRepository.GetVoteListByReplyId(request.CommentId, request.ReplyId);

        var result = new GetReplyVotesListResult(votes.Value);

        Log.Information(
            "{@GetReplyVotesListResult}",
            result);

        return result;
    }
}