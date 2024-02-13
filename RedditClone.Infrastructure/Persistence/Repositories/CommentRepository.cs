namespace RedditClone.Infrastructure.Persistence;

using RedditClone.Application.Persistence;
using RedditClone.Domain.CommentAggregate;

public class CommentRepository : ICommentRepository
{
    private readonly RedditCloneDbContext _dbContext;

    public CommentRepository(RedditCloneDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Comment comment)
    {
        _dbContext.Comments.Add(comment);
        _dbContext.SaveChangesAsync();
    }
}