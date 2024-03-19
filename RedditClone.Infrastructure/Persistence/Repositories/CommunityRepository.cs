namespace RedditClone.Infrastructure.Persistence;

using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RedditClone.Application.Errors;
using RedditClone.Application.Persistence;
using RedditClone.Domain.CommunityAggregate;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public class CommunityRepository : ICommunityRepository
{
    private readonly RedditCloneDbContext _dbContext;

    public CommunityRepository(RedditCloneDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Community GetCommunityById(CommunityId communityId)
    {
        Community community = _dbContext.Communities.FirstOrDefault(c => c.Id == communityId)
            ?? throw new HttpCustomException(
            HttpStatusCode.NotFound, "Community not found");

        return community;
    }

    public Community GetCommunityByName(string name)
    {
        Community community = _dbContext.Communities.FirstOrDefault(c => c.Name == name)
            ?? throw new HttpCustomException(
            HttpStatusCode.NotFound, "Community not found");

        return community;
    }

    public List<Community> GetCommunitiesList()
    {

        List<Community> communities = _dbContext.Communities.ToList();

        return communities;
    }

    public void Add(Community community)
    {
        _dbContext.Communities.Add(community);

        _dbContext.SaveChangesAsync();
    }

    public void UpdateCommunityById(CommunityId id, UserId userId, string name, string description, string topic)
    {
        Community community =
            _dbContext.Communities.SingleOrDefault(c => c.Id == id && c.UserId == userId)
                ?? throw new HttpCustomException(
                HttpStatusCode.NotFound, "Community not found on you communities");

        if (_dbContext.Communities.SingleOrDefault(c => c.Name == name) is not null)
            throw new HttpCustomException(
            HttpStatusCode.Conflict, $"Community with name {name} already exists");

        community.UpdateCommunity(name, description, topic);

        _dbContext.Communities.Update(community);

        _dbContext.Entry(community).State = EntityState.Modified;

        _dbContext.SaveChanges();
    }

    public void DeleteCommunityById(CommunityId id, UserId userId)
    {
        Community community = _dbContext.Communities.SingleOrDefault(c => c.Id == id && c.UserId == userId)
            ?? throw new HttpCustomException(
            HttpStatusCode.NotFound, "Community not found on you communities");

        _dbContext.Communities.Remove(community);

        _dbContext.SaveChanges();
    }

    public void TransferCommunityOwnership(UserId userId, UserId newUserId, CommunityId communityId)
    {
        Community community = _dbContext.Communities.SingleOrDefault(c => c.Id == communityId && c.UserId == userId)
            ?? throw new HttpCustomException(
            HttpStatusCode.NotFound, "Community not found on you communities");

        community.TransferOwnership(newUserId);

        _dbContext.Communities.Update(community);

        _dbContext.Entry(community).State = EntityState.Modified;

        _dbContext.SaveChanges();
    }
}