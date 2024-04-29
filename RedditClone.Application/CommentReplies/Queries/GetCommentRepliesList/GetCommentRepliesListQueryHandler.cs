namespace RedditClone.Application.CommentReplies.Queries.GetCommentRepliesList;

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RedditClone.Application.CommentReplies.Results.GetCommentRepliesListResult;
using RedditClone.Application.Common.Extensions;
using RedditClone.Application.Persistence;

public class GetCommentRepliesListQueryHandler
: IRequestHandler<GetCommentRepliesListQuery, GetCommentRepliesListResult>
{
    private readonly ICommentRepository _commentRepository;

    public GetCommentRepliesListQueryHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<GetCommentRepliesListResult> Handle(
        GetCommentRepliesListQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var commentReplies = _commentRepository.GetCommentById(query.CommentId);

        var replies = commentReplies.Value.Replies.ToList();

        var totalItems = replies.Count;

        var pagedReplies = PaginationHandler.ApplyPagination(
            replies, query.Page, query.PageSize);

        GetCommentRepliesListResult result = new(
            pagedReplies.Item1, pagedReplies.Item2, query.Page, query.PageSize, totalItems);

        return result;
    }
}
