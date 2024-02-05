using RedditClone.Application.Persistence;
using RedditClone.Domain.CommunityAggregate;

namespace RedditClone.Infrastructure.Persistence;

public class CommunityRepository : ICommunityRepository
{
    private static readonly List<CommunityAggregate> _community = new();
    public void Add(CommunityAggregate community)
    {
        _community.Add(community);
    }
}