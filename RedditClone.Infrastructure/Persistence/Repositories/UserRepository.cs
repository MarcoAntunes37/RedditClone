namespace RedditClone.Infrastructure.Persistence.Repositories;

using ErrorOr;
using Serilog;
using BCrypt.Net;
using RedditClone.Domain.UserAggregate;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Persistence;
using RedditClone.Domain.UserAggregate.ValueObjects;

public class UserRepository(
    RedditCloneDbContext dbContext) : IUserRepository
{
    private readonly RedditCloneDbContext _dbContext = dbContext;

    public void Add(User user)
    {
        _dbContext.Users.Add(user);
    }

    public ErrorOr<bool> DeleteUserById(UserId id, UserId requesterId)
    {
        User user = _dbContext.Users.SingleOrDefault(u => u.Id == id)!;

        if (user is null)
        {
            Error error = Errors.User.UserNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        if (user.Id != requesterId)
        {
            Error error = Errors.User.OnlyDeleteSelf;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        _dbContext.Users.Remove(user);

        user.DeleteUser();

        return true;
    }

    public ErrorOr<User> UpdateProfileById(UserId id, string firstname, string lastname, string email)
    {
        User user = _dbContext.Users.SingleOrDefault(u => u.Id == id)!;

        if (user is null)
        {
            Error error = Errors.User.UserNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        user.UpdateProfile(firstname, lastname, email);

        return user;
    }

    public ErrorOr<bool> UpdatePasswordById(UserId id, string oldPassword, string newPassword, string matchPassword)
    {
        User user = _dbContext.Users.SingleOrDefault(u => u.Id == id)!;

        if (user is null)
        {
            Error error = Errors.User.UserNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        if (!BCrypt.Verify(oldPassword, user.Password))
        {
            Error error = Errors.User.InvalidCredentials;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        if (newPassword != matchPassword)
        {
            Error error = Errors.User.PasswordsDoNotMatch;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        user.UpdatePassword(
            BCrypt.HashPassword(
                newPassword, BCrypt.GenerateSalt(), false, HashType.SHA256));

        return true;
    }

    public ErrorOr<bool> UpdateRecoveredPassword(string email, string newPassword, string matchPassword)
    {
        User? user = _dbContext.Users.SingleOrDefault(u => u.Email == email);

        if (user is null)
        {
            Error error = Errors.User.UserNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        if (newPassword != matchPassword)
        {
            Error error = Errors.User.PasswordsDoNotMatch;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        user.UpdatePassword(
            BCrypt.HashPassword(
                newPassword, BCrypt.GenerateSalt(), false, HashType.SHA256));

        return true;
    }

    public User? GetUserByEmail(string email)
    {
        return _dbContext.Users.SingleOrDefault(x => x.Email == email);
    }

    public User? GetUserByUsername(string username)
    {
        return _dbContext.Users.SingleOrDefault(x => x.Username == username);
    }
    public User? GetUserById(UserId userId)
    {
        return _dbContext.Users.SingleOrDefault(x => x.Id == userId);
    }
}