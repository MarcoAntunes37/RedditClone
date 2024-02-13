namespace RedditClone.Infrastructure.Persistence;

using RedditClone.Application.Persistence;
using RedditClone.Domain.CommunityAggregate;

public class CommunityRepository : ICommunityRepository
{
    private readonly RedditCloneDbContext _dbContext;

    public CommunityRepository(RedditCloneDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Community community)
    {
        _dbContext.Communities.Add(community);
        _dbContext.SaveChangesAsync();
    }
}