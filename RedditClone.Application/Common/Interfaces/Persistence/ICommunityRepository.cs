using RedditClone.Domain.CommunityAggregate;

namespace RedditClone.Application.Persistence;

public interface ICommunityRepository
{
    void Add(CommunityAggregate community);
}