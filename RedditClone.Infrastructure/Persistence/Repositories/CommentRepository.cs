namespace RedditClone.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using RedditClone.Application.Persistence;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

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

    public void UpdateCommentById(CommentId id, UserId userId, string content)
    {
        Comment comment = _dbContext.Comments.SingleOrDefault(c => c.Id == id && c.UserId == userId)
            ?? throw new Exception("An error occurred, comment is invalid or you not the owner");

        comment.UpdateComment(content);

        _dbContext.Comments.Update(comment);

        _dbContext.Entry(comment).State = EntityState.Modified;

        _dbContext.SaveChanges();
    }

    public void DeleteCommentById(CommentId id, UserId userId)
    {
        Comment comment = _dbContext.Comments.SingleOrDefault(c => c.Id == id && c.UserId == userId)
            ?? throw new Exception("An error occurred, comment is invalid or you not the owner");

        _dbContext.Comments.Remove(comment);

        _dbContext.SaveChanges();
    }
}