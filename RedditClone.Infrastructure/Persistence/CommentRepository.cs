using RedditClone.Application.Persistence;
using RedditClone.Domain.CommentAggregate;

namespace RedditClone.Infrastructure.Persistence;

public class CommentRepository : ICommentRepository
{
    private static readonly List<CommentAggregate> _comment = new();
    public void Add(CommentAggregate comment)
    {
        _comment.Add(comment);
    }
}