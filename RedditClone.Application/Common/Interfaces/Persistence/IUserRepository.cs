namespace RedditClone.Application.Persistence;

using RedditClone.Domain.UserAggregate;
using RedditClone.Domain.UserAggregate.ValueObjects;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    User? GetUserByUsername(string username);
    void Add(User user);
    void DeleteUserById(UserId id);
    void UpdateProfileById(UserId id, string firstname, string lastname, string email);
    void UpdatePasswordById(UserId id, string oldPassword, string newPassword, string matchPassword);
    void UpdateRecoveredPassword(string email, string newPassword);
}