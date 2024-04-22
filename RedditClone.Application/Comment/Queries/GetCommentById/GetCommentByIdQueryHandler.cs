namespace RedditClone.Application.Comment.Queries.GetCommentById;

using MediatR;
using Serilog;
using RedditClone.Application.Persistence;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Application.Comment.Results.GetCommentByIdResult;

public class GetCommentByIdQueryHandler
: IRequestHandler<GetCommentByIdQuery, GetCommentByIdResult>
{
    private readonly ICommentRepository _commentRepository;

    public GetCommentByIdQueryHandler(
        ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<GetCommentByIdResult> Handle(GetCommentByIdQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@GetCommentByIdQuery}",
            "Trying to retrieve comment data",
            query,
            query.CommentId);

        Comment comment = _commentRepository.GetCommentById(query.CommentId).Value;

        GetCommentByIdResult result = new(comment);

        Log.Information(
            "{@GetCommentByIdResult}",
            result);

        return result;
    }
}
