using System.Linq.Expressions;
using System.Reflection;
using TakeFramework.Domain.Entities;

namespace TakeFramework.Domain.Repositories
{
    /// <summary>
    /// 基础仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseRepository<T, TPrimaryKey> : IBaseRepository<T, TPrimaryKey>
        where T : class, IEntity<TPrimaryKey>
    {

        public abstract bool Any(Expression<Func<T, bool>> predicate);
        public abstract Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        public abstract int Count(Expression<Func<T, bool>> predicate);
        public abstract Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        public abstract T Create(T intput);
        public abstract void Create(List<T> intput);
        public abstract Task<T> CreateAsync(T intput);
        public abstract Task CreateAsync(List<T> intput);
        public abstract void Delete(T intput);
        public abstract void Delete(List<T> intput);
        public abstract void Delete(TPrimaryKey intput);
        public abstract void Delete(List<TPrimaryKey> intput);
        public abstract void Delete(Expression<Func<T, bool>> predicate);
        public abstract Task DeleteAsync(T intput);
        public abstract Task DeleteAsync(List<T> intput);
        public abstract Task DeleteAsync(TPrimaryKey intput);
        public abstract Task DeleteAsync(List<TPrimaryKey> intput);
        public abstract Task DeleteAsync(Expression<Func<T, bool>> predicate);
        public abstract T? FistOrDefault(Expression<Func<T, bool>> predicate);
        public abstract Task<T?> FistOrDefaultAsync(Expression<Func<T, bool>> predicate);
        public abstract List<T> List(bool isTracking = false);
        public abstract List<T> List(Expression<Func<T, bool>> predicate, bool isTracking = false);
        public abstract Task<List<T>> ListAsync(bool isTracking = false);
        public abstract Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate, bool isTracking = false);
        public abstract (List<T>, int) PageList(PageRequest pageRequest, bool isTracking = false);
        public abstract Task<(List<T>, int)> PageListAsync(PageRequest pageRequest, bool isTracking = false);
        public abstract T Single(TPrimaryKey id);
        public abstract Task<T> SingleAsync(TPrimaryKey id);
        public abstract T? SingleOrDefault(TPrimaryKey id);
        public abstract Task<T?> SingleOrDefaultAsync(TPrimaryKey id);

        public abstract T Update(T intput);
        public abstract void Update(List<T> intput);
        public abstract void Update(T intput, Expression<Func<T, bool>> predicate);
        public abstract Task<T> UpdateAsync(T intput);
        public abstract Task UpdateAsync(List<T> intput);
        public abstract Task UpdateAsync(T intput, Expression<Func<T, bool>> predicate);
    }
}
