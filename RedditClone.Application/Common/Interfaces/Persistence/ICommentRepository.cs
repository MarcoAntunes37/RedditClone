namespace RedditClone.Application.Persistence;

using RedditClone.Domain.CommentAggregate;

public interface ICommentRepository
{
    void Add(Comment comment);
}