using RedditClone.Application.Persistence;
using RedditClone.Domain.PostAggregate;

namespace RedditClone.Infrastructure.Persistence;

public class PostRepository : IPostRepository
{
    private static readonly List<PostAggregate> _post = new();
    public void Add(PostAggregate post)
    {
        _post.Add(post);
    }
}