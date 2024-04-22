namespace RedditClone.Application.Comment.Queries.GetPostById;

using MediatR;
using Serilog;
using ErrorOr;
using RedditClone.Domain.Common.Errors;
using RedditClone.Domain.PostAggregate;
using RedditClone.Application.Post.Queries.GetPostById;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.Post.Results.GetPostByIdResult;

public class GetPostByIdQueryHandler(
    IPostRepository postRepository)
: IRequestHandler<GetPostByIdQuery, ErrorOr<GetPostByIdResult>>
{
    private readonly IPostRepository _postRepository = postRepository;

    public async Task<ErrorOr<GetPostByIdResult>> Handle(
        GetPostByIdQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@GetPostByIdQuery}",
            "Trying to retrieve data from post {@PostId}",
            query,
            query.PostId);

        Post post = _postRepository.GetPostById(query.PostId).Value;

        if(post is null)
        {
            Error error = Errors.Posts.PostNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        GetPostByIdResult result = new(post);

        Log.Information(
            "{@GetPostByIdResult}, {@PostId}",
            result,
            query.PostId);

        return result;
    }
}
