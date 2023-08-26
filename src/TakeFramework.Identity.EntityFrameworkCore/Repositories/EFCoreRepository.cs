using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TakeFramework.Domain.Entities;
using TakeFramework.Domain.Repositories;
using TakeFramework.EntityFrameworkCore;

namespace TakeFramework.Identity.Repositories
{
    public class EFCoreRepository<T, TPrimaryKey> : BaseRepository<T, TPrimaryKey>, IBaseRepository<T, TPrimaryKey>
          where T : class, IEntity<TPrimaryKey>, new()
    {
        protected readonly DbContext dbContext;
        protected readonly DbSet<T> dbset;

        protected readonly IdentitySettings IdentitySettings;

        public EFCoreRepository(IEnumerable<IDbContextProvider> dbContextProviders, IOptions<IdentitySettings> identitySettings)
        {
            IdentitySettings = identitySettings.Value;
            this.dbContext = (DbContext)(dbContextProviders.FirstOrDefault(x => x.Name == IdentitySettings.DBName) ?? throw new ArgumentNullException("dbContext is null"));
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
