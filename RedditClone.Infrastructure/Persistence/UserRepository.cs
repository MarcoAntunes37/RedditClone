using RedditClone.Domain.Entities;
using RedditClone.Application.Persistence;

namespace RedditClone.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _user = new();
    public void Add(User user)
    {
        _user.Add(user);
    }

    public User? GetUserByEmail(string email)
    {
        return _user.SingleOrDefault(x => x.Email == email);
    }
}