namespace RedditClone.Infrastructure.Services;

using RedditClone.Application.Common.Interfaces.Services;

public class RecoveryCodeManager : IRecoveryCodeManager
{
    private readonly Dictionary<string, (string code, DateTime expiration)> _recoveryCodes = new Dictionary<string, (string, DateTime)>();

    public Dictionary<string, (string code, DateTime expiration)> RecoveryCodes => _recoveryCodes;

    public void AddCode(string email, string code, DateTime expiration)
    {
        _recoveryCodes[email] = (code, expiration);
    }

    public void RemoveCode(string email)
    {
        if (_recoveryCodes.ContainsKey(email))
        {
            _recoveryCodes.Remove(email);
        }
    }

    public bool ValidateCode(string email, string code)
    {
        if (_recoveryCodes.TryGetValue(email, out var recoveryData))
        {
            if (recoveryData.code == code && recoveryData.expiration > DateTime.UtcNow)
            {
                return true;
            }
            else
            {
                RemoveCode(email);
            }
        }
        return false;
    }
}