namespace RedditClone.Infrastructure.Persistence;

using RedditClone.Application.Persistence;
using RedditClone.Domain.UserAggregate;
using RedditClone.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

public class UserRepository : IUserRepository
{
    private readonly RedditCloneDbContext _dbContext;

    public UserRepository(RedditCloneDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(User user)
    {
        _dbContext.Users.Add(user);

        _dbContext.SaveChanges();
    }

    public void DeleteUserById(UserId id)
    {
        User user = _dbContext.Users.SingleOrDefault(u => u.Id == id)!;

        _dbContext.Users.Remove(user);

        _dbContext.SaveChanges();
    }

    public void UpdateProfileById(UserId id, string firstname, string lastname, string email)
    {
        User user = _dbContext.Users.SingleOrDefault(u => u.Id == id)!;

        user.UpdateProfile(firstname, lastname, email);

        _dbContext.Users.Update(user);
        _dbContext.Entry(user).State = EntityState.Modified;

        _dbContext.SaveChanges();
    }

    public void UpdatePasswordById(UserId id, string oldPassword, string newPassword)
    {
        User user = _dbContext.Users.SingleOrDefault(u => u.Id == id)!;

        if(!BCrypt.Verify(oldPassword, user.Password))
            throw new Exception("Invalid password");

        user.UpdatePassword(BCrypt.HashPassword(
                newPassword, BCrypt.GenerateSalt(), false, HashType.SHA256));

        _dbContext.Users.Update(user);

        _dbContext.Entry(user).State = EntityState.Modified;

        _dbContext.SaveChanges();
    }

    public void UpdateRecoveredPassword(string email, string newPassword)
    {
        User user = _dbContext.Users.SingleOrDefault(u => u.Email == email)!;

        user.UpdatePassword(BCrypt.HashPassword(
                newPassword, BCrypt.GenerateSalt(), false, HashType.SHA256));

        _dbContext.Users.Update(user);

        _dbContext.Entry(user).State = EntityState.Modified;

        _dbContext.SaveChanges();
    }

    public User? GetUserByEmail(string email)
    {
        return _dbContext.Users.SingleOrDefault(x => x.Email == email);
    }

    public User? GetUserByUsername(string username)
    {
        return _dbContext.Users.SingleOrDefault(x => x.Username == username);
    }
}