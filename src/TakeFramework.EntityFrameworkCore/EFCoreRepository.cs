using Microsoft.EntityFrameworkCore;
using System;
using TakeFramework.Domain.Entities;
using TakeFramework.Domain.Repositories;

namespace TakeFramework.EntityFrameworkCore
{
    public class EFCoreRepository<T, TPrimaryKey, TDbContext> : BaseRepository<T, TPrimaryKey>, IBaseRepository<T, TPrimaryKey>
         where T : BaseEntity<TPrimaryKey>, new()
        where TDbContext : DbContext
    {
        public readonly DbContext dbContext;
        public readonly DbSet<T> dbset;

        public EFCoreRepository(IDbContextProvider<TDbContext> dbContextProvider)
        {
            this.dbContext = dbContextProvider.GetDbContext();
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext is null");
            }

            this.dbset = dbContext.Set<T>();
        }
        public override T Create(T intput)
        {
            dbset.Add(intput);
            dbContext.SaveChanges();
            return intput;
        }
        public override void Delete(T intput)
        {
            dbset.Remove(intput);
            dbContext.SaveChanges();
        }

        public override List<T> List()
        {
            return dbset.AsNoTracking().ToList();
        }

        public override T Update(T intput)
        {
            dbset.Update(intput);
            dbContext.SaveChanges();
            return intput;
        }
    }

}