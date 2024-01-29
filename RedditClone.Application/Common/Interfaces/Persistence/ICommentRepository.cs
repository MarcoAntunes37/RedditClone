using RedditClone.Domain.CommentAggregate;
using RedditClone.Domain.PostAggregate;

namespace RedditClone.Application.Persistence;

public interface ICommentRepository
{
    void Add(CommentAggregate comment);
}