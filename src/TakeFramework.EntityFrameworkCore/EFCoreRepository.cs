using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TakeFramework.Domain;
using TakeFramework.Domain.Entities;
using TakeFramework.Domain.Repositories;

namespace TakeFramework.EntityFrameworkCore
{
    public class EFCoreRepository<T, TPrimaryKey, TDbContext> : BaseRepository<T, TPrimaryKey>, IBaseRepository<T, TPrimaryKey>
         where T : BaseEntity<TPrimaryKey>
        where TDbContext : DbContext
    {
        protected readonly DbContext dbContext;
        protected readonly DbSet<T> dbset;

        public EFCoreRepository(IEnumerable<IDbContextProvider> dbContextProviders)
        {
            this.dbContext = (DbContext)(dbContextProviders.FirstOrDefault(x => x.GetType().Equals(typeof(TDbContext))) ?? throw new ArgumentNullException("dbContext is null"));
            this.dbset = dbContext.Set<T>();
        }

        #region  create
        public override T Create(T intput)
        {
            dbset.Add(intput);
            dbContext.SaveChanges();
            return intput;
        }

        public override void Create(List<T> intput)
        {
            dbset.AddRange(intput);
            dbContext.SaveChanges();
        }

        public override async Task<T> CreateAsync(T intput)
        {
            dbset.Add(intput);
            await dbContext.SaveChangesAsync();
            return intput;
        }

        public override Task CreateAsync(List<T> intput)
        {
            dbset.AddRange(intput);
            return dbContext.SaveChangesAsync();
        }

        #endregion


        #region update

        public override T Update(T intput)
        {
            dbset.Update(intput);
            dbContext.SaveChanges();
            return intput;
        }

        public override void Update(List<T> intput)
        {
            dbset.UpdateRange(intput);
            dbContext.SaveChanges();
        }
        /// <summary>
        /// 未实现
        /// </summary>
        /// <param name="intput"></param>
        /// <param name="predicate"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Update(T intput, Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="intput"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Task<T> UpdateAsync(T intput)
        {
            dbset.UpdateRange(intput);
            dbContext.SaveChangesAsync();
            return Task.FromResult(intput);
        }

        public override Task UpdateAsync(List<T> intput)
        {
            dbset.UpdateRange(intput);
            return dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// 未实现
        /// </summary>
        /// <param name="intput"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Task UpdateAsync(T intput, Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region read
        public override T? FistOrDefault(Expression<Func<T, bool>> predicate)
        {
            return dbset.FirstOrDefault(predicate);
        }

        public override Task<T?> FistOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return dbset.FirstOrDefaultAsync(predicate);
        }

        public override List<T> List(bool isTracking = false)
        {
            return isTracking ? dbset.ToList() : dbset.AsNoTracking().ToList();
        }

        public override List<T> List(Expression<Func<T, bool>> predicate, bool isTracking = false)
        {
            return isTracking ? dbset.Where(predicate).ToList() : dbset.AsNoTracking().Where(predicate).ToList();
        }

        public override Task<List<T>> ListAsync(bool isTracking = false)
        {
            return isTracking ? dbset.ToListAsync() : dbset.AsNoTracking().ToListAsync();
        }

        public override Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate, bool isTracking = false)
        {
            return isTracking ? dbset.Where(predicate).ToListAsync() : dbset.AsNoTracking().Where(predicate).ToListAsync();
        }

        public override (List<T>, int) PageList(PageRequest pageRequest, bool isTracking = false)
        {
            return (dbset.OrderBy(pageRequest.OrderField).PageBy(pageRequest.SikpCount, pageRequest.PageSize)
            .Where(pageRequest.GetSql()).ToList(), Count(QueryableExtensions.StringToExpression<T>(pageRequest.GetExpressions())));
        }

        public override async Task<(List<T>, int)> PageListAsync(PageRequest pageRequest, bool isTracking = false)
        {
            return (await dbset.OrderBy(pageRequest.OrderField).PageBy(pageRequest.SikpCount, pageRequest.PageSize)
            .Where(pageRequest.GetSql()).ToListAsync(), await CountAsync(QueryableExtensions.StringToExpression<T>(pageRequest.GetExpressions())));
        }

        public override T Single(TPrimaryKey id)
        {
            return dbset.Single(x => x.Id!.Equals(id));
        }
        public override T? SingleOrDefault(TPrimaryKey id)
        {
            return dbset.SingleOrDefault(x => x.Id!.Equals(id));
        }
        public override Task<T> SingleAsync(TPrimaryKey id)
        {
            return dbset.SingleAsync(x => x.Id!.Equals(id));
        }
        public override Task<T?> SingleOrDefaultAsync(TPrimaryKey id)
        {
            return dbset.SingleOrDefaultAsync(x => x.Id!.Equals(id));
        }

        public override Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return dbset.CountAsync(predicate);
        }
        public override int Count(Expression<Func<T, bool>> predicate)
        {
            return dbset.Count(predicate);
        }
        #endregion
        #region delete
        public override void Delete(T intput)
        {
            dbset.Remove(intput);
            dbContext.SaveChanges();
        }

        public override void Delete(List<T> intput)
        {
            dbset.RemoveRange(intput);
            dbContext.SaveChanges();
        }

        public override void Delete(TPrimaryKey intput)
        {
            var data = SingleOrDefault(intput);
            if (data is null) return;
            dbset.Remove(data);
            dbContext.SaveChanges();
        }

        public override void Delete(List<TPrimaryKey> intput)
        {
            Delete(x => intput.Contains(x.Id));
        }

        public override void Delete(Expression<Func<T, bool>> predicate)
        {
            var data = List(predicate, true);
            if (data is null || !data.Any()) return;
            dbset.RemoveRange(data);
            dbContext.SaveChanges();
        }

        public override Task DeleteAsync(T intput)
        {
            dbset.Remove(intput);
            return dbContext.SaveChangesAsync();
        }

        public override async Task DeleteAsync(List<T> intput)
        {
            dbset.RemoveRange(intput);
            await dbContext.SaveChangesAsync();
        }

        public override async Task DeleteAsync(TPrimaryKey intput)
        {
            var data = await SingleOrDefaultAsync(intput);
            if (data is null) return;
            dbset.Remove(data);
            dbContext.SaveChanges();
        }

        public override Task DeleteAsync(List<TPrimaryKey> intput)
        {
            return DeleteAsync(x => intput.Contains(x.Id));
        }

        public override async Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            var data = await ListAsync(predicate, true);
            if (data is null || !data.Any()) return;
            dbset.RemoveRange(data);
            await dbContext.SaveChangesAsync();
        }

        public override bool Any(Expression<Func<T, bool>> predicate)
        {
            return dbset.Any(predicate);
        }

        public override Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return dbset.AnyAsync(predicate);
        }

        #endregion


    }
}