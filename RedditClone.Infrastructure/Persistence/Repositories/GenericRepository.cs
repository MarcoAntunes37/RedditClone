namespace RedditClone.Infrastructure.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public abstract class GenericRepository<T>
    : IDisposable, IGenericRepository<T> where T : class
{
    private readonly RedditCloneDbContext _dbContext;

    public GenericRepository(RedditCloneDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<T> ListAll()
    {
        IQueryable<T> query = _dbContext.Set<T>();

        return query;
    }

    public IQueryable<T> Find(Expression<Func<T, bool>> predicated)
    {
        IQueryable<T> query = _dbContext.Set<T>().Where(predicated);

        return query;
    }

    public void Add(T entity)
    {
        _dbContext.Set<T>().AddAsync(entity);
    }

    public void Remove(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    public void Update(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }

    public void Dispose()
    {
        _dbContext.DisposeAsync();
    }
}

internal interface IGenericRepository<T> where T : class
{
    IQueryable<T> ListAll();
    IQueryable<T> Find(Expression<Func<T, bool>> predicated);
    void Add(T entity);
    void Remove(T entity);
    void Update(T entity);
    void Save();
    void Dispose();
}