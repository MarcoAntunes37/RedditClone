using RedditClone.Domain.UserAggregate;

namespace RedditClone.Application.Persistence;

public interface IUserRepository
{
    UserAggregate? GetUserByEmail(string email);
    UserAggregate? GetUserByUsername(string username);
    void Add(UserAggregate user);
}