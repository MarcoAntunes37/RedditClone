namespace RedditClone.Application.Comment.Queries.GetPostById;

using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Domain.PostAggregate;
using FluentValidation;
using RedditClone.Application.Post.Queries.GetPostById;
using RedditClone.Application.Post.Results.GetPostByIdResult;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Common.Helpers;
using Serilog;

public class GetPostByIdQueryHandler
: IRequestHandler<GetPostByIdQuery, GetPostByIdResult>
{
    private readonly IPostRepository _postRepository;
    private readonly IValidator<GetPostByIdQuery> _validator;
    private readonly IConfiguration _configuration;

    public GetPostByIdQueryHandler(
        IPostRepository postRepository,
        IValidator<GetPostByIdQuery> validator,
        IConfiguration configuration)
    {
        _validator = validator;
        _postRepository = postRepository;
        _configuration = configuration;
    }

    public async Task<GetPostByIdResult> Handle(GetPostByIdQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@GetPostByIdQuery}",
            "Trying to retrieve data from post {@PostId}",
            query,
            query.PostId);

        _validator.ValidateAndThrow(query);

        Post post = _postRepository.GetPostById(query.PostId);

        GetPostByIdResult result = new(post);

        Log.Information(
            "{@GetPostByIdResult}, {@PostId}",
            result,
            query.PostId);

        return result;
    }
}
