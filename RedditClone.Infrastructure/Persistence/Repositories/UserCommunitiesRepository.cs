using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.UserCommunitiesAggregate;

namespace RedditClone.Infrastructure.Persistence.Repositories;

public class UserCommunitiesRepository : IUserCommunitiesRepository
{
    private readonly RedditCloneDbContext _dbContext;

    public UserCommunitiesRepository(RedditCloneDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(UserCommunities userCommunities)
    {
        _dbContext.UserCommunities.Add(userCommunities);

        _dbContext.SaveChanges();
    }

    public void Remove(UserId userId, CommunityId communityId)
    {
        UserCommunities userCommunities =
            _dbContext.UserCommunities.SingleOrDefault(uc => uc.UserId == userId && uc.CommunityId == communityId)
            ?? throw new Exception("An error occurred violated fk");

        Console.WriteLine(userCommunities);
        _dbContext.UserCommunities.Remove(userCommunities);

        _dbContext.SaveChanges();
    }
}