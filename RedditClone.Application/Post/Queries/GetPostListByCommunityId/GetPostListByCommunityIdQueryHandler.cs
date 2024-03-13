namespace RedditClone.Application.Comment.Queries.GetCommentListByCommunityId;

using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Domain.PostAggregate;
using RedditClone.Application.Community.Queries.GetPostListByCommunityId;
using RedditClone.Application.Post.Results.GetPostListByCommunityIdResult;

public class GetPostListByCommunityIdQueryHandler
: IRequestHandler<GetPostListByCommunityIdQuery, GetPostListByCommunityIdResult>
{
    private readonly IPostRepository _postRepository;

    public GetPostListByCommunityIdQueryHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<GetPostListByCommunityIdResult> Handle(GetPostListByCommunityIdQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        List<Post> posts = _postRepository.GetPostListByCommunity(query.CommunityId);

        int totalItems = posts.Count;
        int startIndex = (query.Page -1) * query.PageSize;
        int endIndex = Math.Min(startIndex + query.PageSize -1, totalItems -1);

        List<Post> pagePosts = posts.GetRange(startIndex, endIndex - startIndex + 1);

        return new GetPostListByCommunityIdResult(
            pagePosts, query.Page, query.PageSize, totalItems);
    }
}