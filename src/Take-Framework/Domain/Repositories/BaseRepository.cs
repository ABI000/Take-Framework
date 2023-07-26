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
        where T : BaseEntity<TPrimaryKey>, new()
    {

        public abstract T Create(T intput);

        public void Create(List<T> intput)
        {
            throw new NotImplementedException();
        }

        public Task<T> CreateAsync(T intput)
        {
            return Task.FromResult(Create(intput));
        }

        public Task CreateAsync(List<T> intput)
        {
            throw new NotImplementedException();
        }

        public abstract void Delete(T intput);

        public void Delete(List<T> intput)
        {
            throw new NotImplementedException();
        }

        public void Delete(TPrimaryKey intput)
        {
            throw new NotImplementedException();
        }

        public void Delete(List<TPrimaryKey> intput)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T intput)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(List<T> intput)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TPrimaryKey intput)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(List<TPrimaryKey> intput)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public T? FistOrDefault(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<T?> FistOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public abstract List<T> List();

        public List<T> List(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public (List<T>, int) PageList(PageRequest pageRequest)
        {
            throw new NotImplementedException();
        }

        public Task<(List<T>, int)> PageListAsync(PageRequest pageRequest)
        {
            throw new NotImplementedException();
        }

        public T Single(TPrimaryKey id)
        {
            throw new NotImplementedException();
        }

        public Task<T> SingleAsync(TPrimaryKey id)
        {
            throw new NotImplementedException();
        }

        public abstract T Update(T intput);
        public void Update(List<T> intput)
        {
            throw new NotImplementedException();
        }

        public void Update(T intput, Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(T intput)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(List<T> intput)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T intput, Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
