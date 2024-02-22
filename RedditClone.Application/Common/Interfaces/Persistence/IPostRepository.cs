namespace RedditClone.Application.Persistence;

using RedditClone.Domain.PostAggregate;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public interface IPostRepository
{
    void Add(Post post);
    void UpdatePostById(PostId id, UserId userId, string title, string content);
    void DeletePostById(PostId id, UserId userId);
}