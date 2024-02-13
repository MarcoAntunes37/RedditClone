namespace RedditClone.Infrastructure.Persistence;

using RedditClone.Application.Persistence;
using RedditClone.Domain.PostAggregate;

public class PostRepository : IPostRepository
{
    private readonly RedditCloneDbContext _dbContext;

    public PostRepository(RedditCloneDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Post post)
    {
        _dbContext.Posts.Add(post);
        _dbContext.SaveChangesAsync();
    }
}