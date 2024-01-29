using RedditClone.Domain.PostAggregate;

namespace RedditClone.Application.Persistence;

public interface IPostRepository
{
    void Add(PostAggregate post);
}