using RedditClone.Application.Persistence;
using RedditClone.Domain.PostAggregate;

namespace RedditClone.Infrastructure.Persistence;

public class PostRepository : IPostRepository
{
    private readonly RedditCloneDbContext _dbContext;

    public PostRepository(RedditCloneDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(PostAggregate post)
    {
        _dbContext.Posts.Add(post);
        _dbContext.SaveChangesAsync();
    }
}