namespace RedditClone.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using RedditClone.Application.Persistence;
using RedditClone.Domain.CommunityAggregate;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

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

    public void UpdateCommunityById(CommunityId id, UserId userId, string name, string description, string topic)
    {
        Community community = _dbContext.Communities.SingleOrDefault(c => c.Id == id && c.UserId == userId)
            ?? throw new Exception("An error occurred, community is invalid or you not the owner");

        community.UpdateCommunity(name, description, topic);

        _dbContext.Communities.Update(community);

        _dbContext.Entry(community).State = EntityState.Modified;

        _dbContext.SaveChanges();
    }

    public void DeleteCommunityById(CommunityId id, UserId userId)
    {
        Community community = _dbContext.Communities.SingleOrDefault(c => c.Id == id && c.UserId == userId)
            ?? throw new Exception("An error occurred, community is invalid or you not the owner");

        _dbContext.Communities.Remove(community);

        _dbContext.SaveChanges();
    }
}