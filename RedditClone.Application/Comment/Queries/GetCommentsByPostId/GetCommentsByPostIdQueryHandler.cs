namespace RedditClone.Application.Comment.Queries.GetCommentsByPostId;

using MediatR;
using Serilog;
using RedditClone.Application.Persistence;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Application.Comment.Results.GetCommentsByPostIdResult;

public class GetCommentsByPostIdQueryHandler
: IRequestHandler<GetCommentsByPostIdQuery, GetCommentsByPostIdResult>
{
    private readonly ICommentRepository _commentRepository;

    public GetCommentsByPostIdQueryHandler(
        ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<GetCommentsByPostIdResult> Handle(GetCommentsByPostIdQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@GetCommentsByPostIdQuery}",
            "Trying to retrieve comments list in Post: {@PostIdp}",
            query,
            query.PostId);

        List<Comment> comments = _commentRepository.GetCommentsListByPostId(query.PostId);

        GetCommentsByPostIdResult result = new(comments);

        Log.Information(
            "{@GetCommentsByPostIdResult}",
            result);

        return result;
    }
}
