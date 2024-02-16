namespace RedditClone.Application.Persistence;

using RedditClone.Domain.CommunityAggregate;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

public interface ICommunityRepository
{
    void Add(Community community);
    void UpdateCommunityById(CommunityId id, UserId userId, string name, string description, string topic);
    void DeleteCommunityById(CommunityId id, UserId userId);
}