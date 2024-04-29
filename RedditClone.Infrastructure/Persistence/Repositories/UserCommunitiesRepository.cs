namespace RedditClone.Infrastructure.Persistence.Repositories;

using ErrorOr;
using Serilog;
using RedditClone.Domain.Common.Errors;
using RedditClone.Domain.UserAggregate;
using RedditClone.Domain.CommunityAggregate;
using RedditClone.Domain.UserCommunitiesAggregate;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.UserCommunitiesAggregate.Enum;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.Common.Interfaces.Persistence;

public class UserCommunitiesRepository(RedditCloneDbContext dbContext)
    : IUserCommunitiesRepository
{
    private readonly RedditCloneDbContext _dbContext = dbContext;

#pragma warning disable CS8603
    public UserCommunities? GetUserCommunities(UserId userId, CommunityId communityId)
    {
        UserCommunities userCommunities = _dbContext.UserCommunities
            .SingleOrDefault(uc => uc.UserId == userId && uc.CommunityId == communityId)!;

        return userCommunities;
    }
#pragma warning restore CS8603

    public UserCommunities GetUserCommunitiesAdmin(CommunityId communityId)
    {
        UserCommunities? userCommunities =
            _dbContext.UserCommunities.FirstOrDefault(
                uc => uc.CommunityId == communityId && uc.Role == 0);

        return userCommunities!;
    }

    public void Add(UserCommunities userCommunities)
    {
        _dbContext.UserCommunities.Add(userCommunities);
    }

    public ErrorOr<bool> UpdateRole(UserId userId, CommunityId communityId, Role role)
    {
        var userCommunitiesData = _dbContext.UserCommunities.FirstOrDefault(
            uc => uc.UserId == userId &&
            uc.CommunityId == communityId);

        if (userCommunitiesData is null)
        {
            Error error = Errors.UserCommunities.UserNotInCommunity;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        userCommunitiesData.UpdateRole(role);

        _dbContext.Update(userCommunitiesData);

        return true;
    }

    public ErrorOr<bool> Remove(UserId userId, CommunityId communityId)
    {
        UserCommunities? userCommunities = _dbContext.UserCommunities
            .SingleOrDefault(uc => uc.UserId == userId && uc.CommunityId == communityId);

        if (userCommunities is null)
        {
            Error error = Errors.UserCommunities.UserNotInCommunity;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        _dbContext.UserCommunities.Remove(userCommunities);

        return true;
    }

    public List<User> GetUsersListByCommunityId(CommunityId communityId)
    {
        List<User> users = _dbContext.UserCommunities
            .Where(uc => uc.CommunityId == communityId)
            .Join(_dbContext.Users,
                userCommunities => userCommunities.UserId,
                user => user.Id,
                (userCommunities, user) => user)
            .ToList();

        return users;
    }

    public List<Community> GetCommunitiesListByUserId(UserId userId)
    {
        List<Community> communities = _dbContext.UserCommunities
            .Where(uc => uc.UserId == userId)
            .Join(_dbContext.Communities,
                userCommunities => userCommunities.CommunityId,
                community => community.Id,
                (userCommunities, community) => community)
            .ToList();

        return communities;
    }

    public bool UserExists(UserId userId)
    {
        return _dbContext.Users.Any(u => u.Id == userId);
    }

    public bool CommunityExists(CommunityId communityId)
    {
        return _dbContext.Communities.Any(c => c.Id == communityId);
    }
}