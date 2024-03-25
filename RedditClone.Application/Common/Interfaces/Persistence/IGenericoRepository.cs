using System.Linq.Expressions;

namespace RedditClone.Application.Persistence;


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