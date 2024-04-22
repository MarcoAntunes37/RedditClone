namespace RedditClone.Application.Comment.Queries.GetPostListByCommunityId;

using MediatR;
using Serilog;
using RedditClone.Domain.PostAggregate;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.Community.Queries.GetPostListByCommunityId;
using RedditClone.Application.Post.Results.GetPostListByCommunityIdResult;

public class GetPostListByCommunityIdQueryHandler(
    IPostRepository postRepository)
: IRequestHandler<GetPostListByCommunityIdQuery, GetPostListByCommunityIdResult>
{
    private readonly IPostRepository _postRepository = postRepository;

    public async Task<GetPostListByCommunityIdResult> Handle(GetPostListByCommunityIdQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@GetPostListByCommunityIdQuery}",
            "Trying to retrieve list of posts of Community: {@CommunityId}",
            query.CommunityId);

        List<Post> posts = _postRepository.GetPostListByCommunity(query.CommunityId);

        int totalItems = posts.Count;
        int startIndex = (query.Page -1) * query.PageSize;
        int endIndex = Math.Min(startIndex + query.PageSize -1, totalItems -1);

        List<Post> pagePosts = posts.GetRange(startIndex, endIndex - startIndex + 1);

        GetPostListByCommunityIdResult result = new (
            pagePosts, query.Page, query.PageSize, totalItems);

        Log.Information(
            "{@GetPostListByCommunityIdResult}, {@CommunityId}",
            query,
            query.CommunityId);

        return result;
    }
}