namespace RedditClone.Application.Comment.Queries.GetCommentById;

using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Domain.CommentAggregate;
using FluentValidation;
using RedditClone.Application.Comment.Results.GetCommentByIdResult;

public class GetCommentByIdQueryHandler
: IRequestHandler<GetCommentByIdQuery, GetCommentByIdResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<GetCommentByIdQuery> _validator;

    public GetCommentByIdQueryHandler(
        ICommentRepository commentRepository,
        IValidator<GetCommentByIdQuery> validator)
    {
        _validator = validator;
        _commentRepository = commentRepository;
    }

    public async Task<GetCommentByIdResult> Handle(GetCommentByIdQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(query);

        Comment comment = _commentRepository.GetCommentById(query.CommentId);

        return new GetCommentByIdResult(comment);
    }
}
