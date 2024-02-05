using RedditClone.Application.Persistence;
using RedditClone.Domain.UserAggregate;

namespace RedditClone.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly RedditCloneDbContext _dbContext;

    public UserRepository(RedditCloneDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(UserAggregate user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChangesAsync();
    }

    public UserAggregate? GetUserByEmail(string email)
    {
        return _dbContext.Users.SingleOrDefault(x => x.Email == email);
    }

    public UserAggregate? GetUserByUsername(string username)
    {
        return _dbContext.Users.SingleOrDefault(x => x.Username == username);
    }
}