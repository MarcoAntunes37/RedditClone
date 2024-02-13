namespace RedditClone.Application.Persistence;

using RedditClone.Domain.PostAggregate;

public interface IPostRepository
{
    void Add(Post post);
}