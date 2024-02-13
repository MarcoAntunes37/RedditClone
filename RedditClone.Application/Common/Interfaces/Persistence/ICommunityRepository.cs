namespace RedditClone.Application.Persistence;

using RedditClone.Domain.CommunityAggregate;

public interface ICommunityRepository
{
    void Add(Community community);
}