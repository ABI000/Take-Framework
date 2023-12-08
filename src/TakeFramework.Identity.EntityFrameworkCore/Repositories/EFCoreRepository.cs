using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TakeFramework.Domain.Entities;
using TakeFramework.Domain.Repositories;
using TakeFramework.EntityFrameworkCore;

namespace TakeFramework.Identity.Repositories
{
    public class EFCoreRepository<T, TPrimaryKey> : EntityFrameworkCore.EFCoreRepository<T, TPrimaryKey, DbContext>, IBaseRepository<T, TPrimaryKey>
          where T : class, IEntity<TPrimaryKey>, new()
    {
        protected readonly new DbContext dbContext;
        protected readonly new DbSet<T> dbset;

        protected readonly IdentitySettings IdentitySettings;

        public EFCoreRepository(IEnumerable<IDbContextProvider> dbContextProviders, IOptions<IdentitySettings> identitySettings, EntityDictionary entityDictionary) : base(dbContextProviders, entityDictionary)
        {
            IdentitySettings = identitySettings.Value;
            this.dbContext = (DbContext)(dbContextProviders.FirstOrDefault(x => x.Name == IdentitySettings.DBName) ?? throw new ArgumentNullException("dbContext is null"));
            this.dbset = dbContext.Set<T>();
        }


    }

}
