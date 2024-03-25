namespace RedditClone.Infrastructure.Persistence;

internal class UnitOfWork : IUnitOfWork
{
    private readonly RedditCloneDbContext _dbContext;

    public UnitOfWork(RedditCloneDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task SaveChanges(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
internal interface IUnitOfWork
{
    Task SaveChanges(CancellationToken cancellationToken = default);
}