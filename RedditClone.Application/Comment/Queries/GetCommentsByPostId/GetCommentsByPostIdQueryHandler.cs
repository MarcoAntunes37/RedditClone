namespace RedditClone.Application.Comment.Queries.GetCommentsByPostId;

using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Domain.CommentAggregate;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Common.Helpers;
using Serilog;
using RedditClone.Application.Comment.Results.GetCommentsByPostIdResult;

public class GetCommentsByPostIdQueryHandler
: IRequestHandler<GetCommentsByPostIdQuery, GetCommentsByPostIdResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<GetCommentsByPostIdQuery> _validator;
    private readonly IConfiguration _configuration;

    public GetCommentsByPostIdQueryHandler(
        ICommentRepository commentRepository,
        IValidator<GetCommentsByPostIdQuery> validator,
        IConfiguration configuration)
    {
        _validator = validator;
        _commentRepository = commentRepository;
        _configuration = configuration;
    }

    public async Task<GetCommentsByPostIdResult> Handle(GetCommentsByPostIdQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@GetCommentsByPostIdQuery}",
            "Trying to retrieve comments list in Post: {@PostId}",
            query,
            query.PostId);

        _validator.ValidateAndThrow(query);

        List<Comment> comments = _commentRepository.GetCommentsListByPostId(query.PostId);

        GetCommentsByPostIdResult result = new(comments);

        Log.Information(
            "{@GetCommentsByPostIdResult}",
            result);

        return result;
    }
}
