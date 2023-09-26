using Microsoft.EntityFrameworkCore;
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
        public override T Create(T intput)
        {
            dbset.Add(intput);
            dbContext.SaveChanges();
            return intput;
        }
        public override void Delete(T intput)
        {
            dbset.Remove(intput);
        }

        public override List<T> List()
        {
            return dbset.AsNoTracking().ToList();
        }

        public override T Update(T intput)
        {
            dbset.Update(intput);
            return intput;
        }
    }
}