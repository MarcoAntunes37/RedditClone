namespace RedditClone.Application.Settings;

public class SerilogSettings
{
    public const string SectionName = "SerilogSettings";
    public List<string> Using = null!;
    public string MinimumLevel = null!;
    public List<string> WriteTo = null!;
    public List<string> Enrich = null!;
}