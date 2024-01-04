using RedditClone.Domain.Entities;

namespace RedditClone.Application.Persistence;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);
}