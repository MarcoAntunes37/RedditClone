namespace RedditClone.Application.Persistence;

using ErrorOr;
using RedditClone.Domain.CommunityAggregate;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

public interface ICommunityRepository
{
    ErrorOr<Community> GetCommunityById(CommunityId communityId);
    ErrorOr<Community> GetCommunityByName(string name);
    List<Community> GetCommunitiesList();
    void Add(Community community);
    ErrorOr<bool> UpdateCommunityById(CommunityId id, UserId userId, string name, string description, string topic);
    ErrorOr<bool> DeleteCommunityById(CommunityId id, UserId userId);
}