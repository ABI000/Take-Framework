using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TakeFramework.Domain;
using TakeFramework.Domain.Entities;
using TakeFramework.Domain.Repositories;

namespace TakeFramework.EntityFrameworkCore
{

    public class EFCoreRepository<T, TPrimaryKey, TDbContext> : BaseRepository<T, TPrimaryKey>, IBaseRepository<T, TPrimaryKey>
             where T : class, IEntity<TPrimaryKey>
            where TDbContext : DbContext
    {
        protected readonly DbContext dbContext;
        protected readonly DbSet<T> dbset;

        protected readonly IQueryable<T> Queryable;
        protected readonly EntityDictionary entityDictionary;
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
            return intput;
        }

        public override void Create(List<T> intput)
        {
            dbset.AddRange(intput);
        }

        public override Task<T> CreateAsync(T intput)
        {
            dbset.Add(intput);
            return Task.FromResult(intput);
        }

        public override Task CreateAsync(List<T> intput)
        {
            dbset.AddRange(intput);
            return Task.CompletedTask;
        }

        #endregion


        #region update

        public override T Update(T intput)
        {
            dbset.Update(intput);
            return intput;
        }

        public override void Update(List<T> intput)
        {
            dbset.UpdateRange(intput);
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
            return Task.FromResult(intput);
        }

        public override Task UpdateAsync(List<T> intput)
        {
            dbset.UpdateRange(intput);
            return Task.CompletedTask;
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
            return isTracking ? dbset.WhereIF(predicate).ToList() : dbset.AsNoTracking().WhereIF(predicate).ToList();
        }

        public override Task<List<T>> ListAsync(Expression<Func<T, bool>>? predicate = null, bool isTracking = false)
        {
            return isTracking ? dbset.WhereIF(predicate).ToListAsync() : dbset.AsNoTracking().WhereIF(predicate).ToListAsync();
        }

        public override (List<T> data, int totalCount) PageList(PageRequest pageRequest, bool isTracking = false)
        {
            if (pageRequest.Conditions.Count != 0)
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
            var typeName = nameof(T);
            conditions = conditions.Select(x =>
            {
                if (x.expression.StartsWith("@0"))
                {
                    x.value = entityDictionary.ChangeType(typeName, x.name, x.value);
                }
                return x;
            });

            return QueryableExtensions.ConditionToExpression<T>(conditions.Select(x => (1, x.expression, entityDictionary.ChangeType(typeName, x.name, x.value)))!);
        }


        #endregion

        public override async Task<(List<T> data, int totalCount)> PageListAsync(PageRequest pageRequest, bool isTracking = false)
        {
            if (pageRequest.Conditions.Count != 0)
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
            return (await dbset.WhereIF(predicate).OrderBy(orderField).PageBy(GetSikpCount(pageIndex, pageSize), pageSize).ToListAsync(), await CountAsync(predicate));
        }
        public override (List<T> data, int totalCount) PageList(int pageIndex, int pageSize, string orderField, Expression<Func<T, bool>>? predicate = null)
        {
            return (dbset.WhereIF(predicate).OrderBy(orderField).PageBy(GetSikpCount(pageIndex, pageSize), pageSize).ToList(), Count(predicate));
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
            return dbset.WhereIF(predicate).CountAsync();
        }
        public override int Count(Expression<Func<T, bool>>? predicate = null)
        {
            return dbset.WhereIF(predicate).Count();
        }
        #endregion
        #region delete
        public override void Delete(T intput)
        {
            dbset.Remove(intput);
        }

        public override void Delete(List<T> intput)
        {
            dbset.RemoveRange(intput);
        }

        public override void Delete(TPrimaryKey intput)
        {
            var data = SingleOrDefault(intput);
            if (data is null) return;
            dbset.Remove(data);
        }

        public override void Delete(List<TPrimaryKey> intput)
        {
            Delete(x => intput.Contains(x.Id));
        }

        public override void Delete(Expression<Func<T, bool>> predicate)
        {
            var data = List(predicate, true);
            if (data is null || data.Count == 0) return;
            dbset.RemoveRange(data);
        }

        public override Task DeleteAsync(T intput)
        {
            dbset.Remove(intput);
            return Task.CompletedTask;
        }

        public override Task DeleteAsync(List<T> intput)
        {
            dbset.RemoveRange(intput);
            return Task.CompletedTask;
        }

        public override async Task DeleteAsync(TPrimaryKey intput)
        {
            var data = await SingleOrDefaultAsync(intput);
            if (data is null) return;
            dbset.Remove(data);
        }

        public override Task DeleteAsync(List<TPrimaryKey> intput)
        {
            return DeleteAsync(x => intput.Contains(x.Id));
        }

        public override async Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            var data = await ListAsync(predicate, true);
            if (data is null || data.Count == 0) return;
            dbset.RemoveRange(data);
            await Task.CompletedTask;
        }

        public override bool Any(Expression<Func<T, bool>>? predicate = null)
        {
            return dbset.WhereIF(predicate).Any();
        }

        public override Task<bool> AnyAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return dbset.WhereIF(predicate).AnyAsync();
        }

        public override T Single(Expression<Func<T, bool>>? predicate = null)
        {
            return dbset.WhereIF(predicate).Single();
        }

        public override Task<T> SingleAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return dbset.WhereIF(predicate).SingleAsync();
        }

        public override T? SingleOrDefault(Expression<Func<T, bool>>? predicate = null)
        {
            return dbset.WhereIF(predicate).SingleOrDefault();
        }

        public override Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return dbset.WhereIF(predicate).SingleOrDefaultAsync();
        }

        #endregion


    }

}