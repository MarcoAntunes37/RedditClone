namespace RedditClone.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using RedditClone.Application.Persistence;
using RedditClone.Domain.PostAggregate;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

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

    public void UpdatePostById(PostId id, UserId userId, string title, string content)
    {
        Post post = _dbContext.Posts.SingleOrDefault(c => c.Id == id && c.UserId == userId)
            ?? throw new Exception("An error occurred, post is invalid or you not the owner");

        post.UpdatePost(title, content);

        _dbContext.Posts.Update(post);

        _dbContext.Entry(post).State = EntityState.Modified;

        _dbContext.SaveChanges();
    }

    public void DeletePostById(PostId id, UserId userId)
    {
        Post post = _dbContext.Posts.SingleOrDefault(c => c.Id == id && c.UserId == userId)
            ?? throw new Exception("An error occurred, post is invalid or you not the owner");

        _dbContext.Posts.Remove(post);

        _dbContext.SaveChanges();
    }
}