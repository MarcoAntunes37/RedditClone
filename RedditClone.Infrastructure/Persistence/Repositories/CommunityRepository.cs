namespace RedditClone.Infrastructure.Persistence.Repositories;

using Serilog;
using ErrorOr;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Persistence;
using RedditClone.Domain.CommunityAggregate;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public class CommunityRepository(RedditCloneDbContext dbContext) : ICommunityRepository
{
    private readonly RedditCloneDbContext _dbContext = dbContext;

    public ErrorOr<Community> GetCommunityById(CommunityId communityId)
    {
        Community? community = _dbContext.Communities.FirstOrDefault(c => c.Id == communityId);

        if(community is null)
        {
            Error error = Errors.Community.CommunityNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        return community;
    }

    public ErrorOr<Community> GetCommunityByName(string name)
    {
        Community? community = _dbContext.Communities.FirstOrDefault(c => c.Name == name);

        if(community is null)
        {
            Error error = Errors.Community.CommunityNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);
        }

        return community!;
    }

    public List<Community> GetCommunitiesList()
    {
        List<Community> communities = _dbContext.Communities.ToList();

        return communities;
    }

    public void Add(Community community)
    {
        _dbContext.Communities.Add(community);
    }

    public ErrorOr<bool> UpdateCommunityById(CommunityId id, UserId userId, string name, string description, string topic)
    {
        Community? community =
            _dbContext.Communities.SingleOrDefault(c => c.Id == id);

        if(community is null)
        {
            Error error = Errors.Community.CommunityNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        if (_dbContext.Communities.SingleOrDefault(c => c.Name == name) is not null)
        {
            Error error = Errors.Community.CommunityNameAlreadyExists;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        community.UpdateCommunity(name, description, topic);

        _dbContext.Communities.Update(community);

        return true;
    }

    public ErrorOr<bool> DeleteCommunityById(CommunityId id, UserId userId)
    {
        Community? community = _dbContext.Communities.SingleOrDefault(c => c.Id == id);

        if (community is null)
        {
            Error error = Errors.Community.CommunityNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        _dbContext.Communities.Remove(community);

        community.DeleteCommunity();

        return true;
    }
}