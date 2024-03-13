namespace RedditClone.Application.Comment.Queries.GetCommunityById;

using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Domain.PostAggregate;
using FluentValidation;
using RedditClone.Application.Post.Queries.GetPostById;
using RedditClone.Application.Post.Results.GetPostByIdResult;

public class GetPostByIdQueryHandler
: IRequestHandler<GetPostByIdQuery, GetPostByIdResult>
{
    private readonly IPostRepository _postRepository;
    private readonly IValidator<GetPostByIdQuery> _validator;

    public GetPostByIdQueryHandler(
        IPostRepository postRepository,
        IValidator<GetPostByIdQuery> validator)
    {
        _validator = validator;
        _postRepository = postRepository;
    }

    public async Task<GetPostByIdResult> Handle(GetPostByIdQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(query);

        Post post = _postRepository.GetPostById(query.PostId);

        return new GetPostByIdResult(post);
    }
}
