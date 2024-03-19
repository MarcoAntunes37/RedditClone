namespace RedditClone.Infrastructure.Settings;

public class DbSettings
{
    public const string SectionName = "DbSettings";
    public string Host { get; init; } = null!;
    public int Port { get; init; } = 0;
    public string DB { get; init; } = null!;
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
}