namespace RedditClone.Application.Comment.Queries.GetCommentsByPostId;

using MediatR;
using Serilog;
using RedditClone.Application.Persistence;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Application.Comment.Queries.GetCommentsListByPostId;
using RedditClone.Application.Comment.Results.GetCommentsListByPostIdResults;
using RedditClone.Application.Common.Extensions;

public class GetCommentsByPostIdQueryHandler
: IRequestHandler<GetCommentsListByPostIdQuery, GetCommentsListByPostIdResult>
{
    private readonly ICommentRepository _commentRepository;

    public GetCommentsByPostIdQueryHandler(
        ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<GetCommentsListByPostIdResult> Handle(
        GetCommentsListByPostIdQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@GetCommentsByPostIdQuery}",
            "Trying to retrieve comments list in Post: {@PostIdp}",
            query,
            query.PostId);

        List<Comment> comments = _commentRepository.GetCommentsListByPostId(query.PostId);

        var totalItems = comments.Count;

        var pagedComments = PaginationHandler.ApplyPagination(comments, query.Page, query.PageSize);

        GetCommentsListByPostIdResult result = new(
            pagedComments.Item1,
            pagedComments.Item2,
            totalItems,
            query.Page,
            query.PageSize
        );

        Log.Information(
            "{@GetCommentsByPostIdResult}",
            result);

        return result;
    }
}
