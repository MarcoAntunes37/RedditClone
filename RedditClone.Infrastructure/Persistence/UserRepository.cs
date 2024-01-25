using RedditClone.Application.Persistence;
using RedditClone.Domain.UserAggregate;

namespace RedditClone.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private static readonly List<UserAggregate> _user = new();
    public void Add(UserAggregate user)
    {
        _user.Add(user);
    }

    public UserAggregate? GetUserByEmail(string email)
    {
        return _user.SingleOrDefault(x => x.Email == email);
    }

    public UserAggregate? GetUserByUsername(string username)
    {
        return _user.SingleOrDefault(x => x.Username == username);
    }
}