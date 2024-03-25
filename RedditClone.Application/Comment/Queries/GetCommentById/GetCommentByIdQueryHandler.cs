namespace RedditClone.Application.Comment.Queries.GetCommentById;

using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Domain.CommentAggregate;
using FluentValidation;
using RedditClone.Application.Comment.Results.GetCommentByIdResult;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Common.Helpers;
using Serilog;

public class GetCommentByIdQueryHandler
: IRequestHandler<GetCommentByIdQuery, GetCommentByIdResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<GetCommentByIdQuery> _validator;
    private readonly IConfiguration _configuration;

    public GetCommentByIdQueryHandler(
        ICommentRepository commentRepository,
        IValidator<GetCommentByIdQuery> validator,
        IConfiguration configuration)
    {
        _validator = validator;
        _commentRepository = commentRepository;
        _configuration = configuration;
    }

    public async Task<GetCommentByIdResult> Handle(GetCommentByIdQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@GetCommentByIdQuery}",
            "Trying to retrieve comment data",
            query,
            query.CommentId);

        _validator.ValidateAndThrow(query);

        Comment comment = _commentRepository.GetCommentById(query.CommentId);

        GetCommentByIdResult result = new(comment);

        Log.Information(
            "{@GetCommentByIdResult}",
            result);

        return result;
    }
}
