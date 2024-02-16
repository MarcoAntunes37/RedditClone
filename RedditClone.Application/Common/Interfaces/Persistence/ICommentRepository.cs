namespace RedditClone.Application.Persistence;

using RedditClone.Domain.CommentAggregate;
using RedditClone.Domain.CommentAggregate.ValueObjects;

public interface ICommentRepository
{
    void Add(Comment comment);
    void UpdateCommentById(CommentId id, UserId userId, string content);
    void DeleteCommentById(CommentId id, UserId userId);
}