using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TakeFramework.Domain;
using TakeFramework.Domain.Entities;
using TakeFramework.Domain.Repositories;

namespace TakeFramework.EntityFrameworkCore
{

    public class EFCoreRepository<T, TPrimaryKey, TDbContext> : BaseRepository<T, TPrimaryKey>, IBaseRepository<T, TPrimaryKey>
             where T : class, IEntity<TPrimaryKey>, new()
            where TDbContext : DbContext
    {
        protected readonly DbContext dbContext;
        protected readonly DbSet<T> dbset;

        protected readonly IQueryable<T> Queryable;
        protected readonly EntityDictionary entityDictionary;
        private readonly IEnumerable<IDbContextProvider> dbContextProviders;

        public EFCoreRepository(IEnumerable<IDbContextProvider> dbContextProviders, EntityDictionary entityDictionary)
        {
            this.entityDictionary = entityDictionary;
            this.dbContext = (DbContext)(dbContextProviders.FirstOrDefault(x => x.GetType().Equals(typeof(TDbContext))) ?? throw new ArgumentNullException("dbContext is null"));
            this.dbset = dbContext.Set<T>();
            Queryable = dbset;
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

        public override List<T> List(Expression<Func<T, bool>>? predicate = null, bool isTracking = false)
        {
            return isTracking ? dbset.WhereIF(predicate is not null, predicate).ToList() : dbset.AsNoTracking().WhereIF(predicate is not null, predicate).ToList();
        }

        public override Task<List<T>> ListAsync(Expression<Func<T, bool>>? predicate = null, bool isTracking = false)
        {
            return isTracking ? dbset.WhereIF(predicate is not null, predicate).ToListAsync() : dbset.AsNoTracking().WhereIF(predicate is not null, predicate).ToListAsync();
        }

        public override (List<T> data, int totalCount) PageList(PageRequest pageRequest, bool isTracking = false)
        {
            if (pageRequest.Conditions.Any())
            {
                var conditions = GetConditions(pageRequest.GetExpressions());
                return (dbset.Where(conditions).OrderBy(pageRequest.OrderField).PageBy(pageRequest.SikpCount, pageRequest.PageSize).ToList(), Count(conditions));
            }
            else
            {
                return (dbset.OrderBy(pageRequest.OrderField).PageBy(pageRequest.SikpCount, pageRequest.PageSize).ToList(), Count());
            }
        }

        #region 条件转换


        private Expression<Func<T, bool>> GetConditions(IEnumerable<(string expression, string name, object? value)> conditions)
        {
            conditions = conditions.Select(x =>
            {
                if (x.expression.StartsWith("@0"))
                {
                    x.value = ChangeType(x.name, x.value);
                }
                return x;
            });

            return QueryableExtensions.ConditionToExpression<T>(conditions.Select(x => (1, x.expression, ChangeType(x.name, x.value)))!);
        }
        //处理类型转换
        protected object? ChangeType(string name, object? value)
        {
            var type = entityDictionary.GetType(nameof(T), name);
            if (type.Equals("int", StringComparison.CurrentCultureIgnoreCase))
            {
                return ((List<string>)value!).Select(x => int.Parse(x));

            }
            else if (type.Equals("long", StringComparison.CurrentCultureIgnoreCase))
            {
                return ((List<string>)value!).Select(x => long.Parse(x));
            }
            return value;
        }


        #endregion

        public override async Task<(List<T> data, int totalCount)> PageListAsync(PageRequest pageRequest, bool isTracking = false)
        {
            if (pageRequest.Conditions.Any())
            {
                var conditions = GetConditions(pageRequest.GetExpressions());
                return (await dbset.Where(conditions).OrderBy(pageRequest.OrderField).PageBy(pageRequest.SikpCount, pageRequest.PageSize)
         .ToListAsync(), await CountAsync(conditions));
            }
            else
            {

                return (await dbset.OrderBy(pageRequest.OrderField).PageBy(pageRequest.SikpCount, pageRequest.PageSize)
                .ToListAsync(), await CountAsync());
            }
        }

        public static int GetSikpCount(int pageIndex, int pageSize)
        {
            return ((pageIndex < 1 ? 1 : pageIndex) - 1) * pageSize;
        }
        public async override Task<(List<T> data, int totalCount)> PageListAsync(int pageIndex, int pageSize, string orderField, Expression<Func<T, bool>>? predicate = null)
        {
            return (await dbset.WhereIF(predicate is not null, predicate).OrderBy(orderField).PageBy(GetSikpCount(pageIndex, pageSize), pageSize).ToListAsync(), await CountAsync(predicate));
        }
        public override (List<T> data, int totalCount) PageList(int pageIndex, int pageSize, string orderField, Expression<Func<T, bool>>? predicate = null)
        {
            return (dbset.WhereIF(predicate is not null, predicate).OrderBy(orderField).PageBy(GetSikpCount(pageIndex, pageSize), pageSize).ToList(), Count(predicate));
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

        public override Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return dbset.WhereIF(predicate is not null, predicate).CountAsync();
        }
        public override int Count(Expression<Func<T, bool>>? predicate = null)
        {
            return dbset.WhereIF(predicate is not null, predicate).Count();
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

        public override bool Any(Expression<Func<T, bool>>? predicate = null)
        {
            return dbset.WhereIF(predicate is not null, predicate).Any();
        }

        public override Task<bool> AnyAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return dbset.WhereIF(predicate is not null, predicate).AnyAsync();
        }

        public override T Single(Expression<Func<T, bool>> predicate)
        {
            return dbset.Single(predicate);
        }

        public override Task<T> SingleAsync(Expression<Func<T, bool>> predicate)
        {
            return dbset.SingleAsync(predicate);
        }

        public override T? SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return dbset.SingleOrDefault(predicate);
        }

        public override Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return dbset.SingleOrDefaultAsync(predicate);
        }

        #endregion


        public Task BeginTransactionAsync()
        {

            return dbContext.Database.BeginTransactionAsync();
        }
        public Task CommitTransactionAsync()
        {

            return dbContext.Database.CommitTransactionAsync();
        }
        public Task RollbackTransactionAsync()
        {
            return dbContext.Database.RollbackTransactionAsync();
        }

        public void BeginTransaction()
        {

            dbContext.Database.BeginTransaction();
        }
        public void CommitTransaction()
        {

            dbContext.Database.CommitTransaction();
        }
        public void RollbackTransaction()
        {
            dbContext.Database.RollbackTransaction();
        }
    }

}