using RedditClone.Application.Persistence;
using RedditClone.Domain.CommunityAggregate;

namespace RedditClone.Infrastructure.Persistence;

public class CommunityRepository : ICommunityRepository
{
    private readonly RedditCloneDbContext _dbContext;

    public CommunityRepository(RedditCloneDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(CommunityAggregate community)
    {
        _dbContext.Communities.Add(community);
        _dbContext.SaveChangesAsync();
    }
}