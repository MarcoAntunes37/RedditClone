namespace RedditClone.Application.Persistence;

using ErrorOr;
using RedditClone.Domain.UserAggregate;
using RedditClone.Domain.UserAggregate.ValueObjects;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    User? GetUserById(UserId userId);
    User? GetUserByUsername(string username);
    void Add(User user);
    ErrorOr<bool> DeleteUserById(UserId id, UserId requesterId);
    ErrorOr<User> UpdateProfileById(UserId id, string firstname, string lastname, string email);
    ErrorOr<bool> UpdatePasswordById(UserId id, string oldPassword, string newPassword, string matchPassword);
    ErrorOr<bool> UpdateRecoveredPassword(string email, string newPassword, string matchPassword);
}