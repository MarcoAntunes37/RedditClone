namespace RedditClone.Application.Common.Interfaces.Services;

public interface IRecoveryCodeManager
{
    void AddCode(string email, string code, DateTime expiration);
    void RemoveCode(string email);
    bool ValidateCode(string email, string code);
}