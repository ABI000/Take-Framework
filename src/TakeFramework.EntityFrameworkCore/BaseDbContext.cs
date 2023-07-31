using Microsoft.EntityFrameworkCore;

namespace TakeFramework.EntityFrameworkCore
{
    public abstract class BaseDbContext<TDbContext> : DbContext, IDbContextProvider
        where TDbContext : DbContext
    {
        public abstract string Name { get; }
        public abstract TDbContext GetDbContext();
        public virtual Task<TDbContext> GetDbContextAsync()
        {

            return Task.FromResult(GetDbContext());
        }
    }
}
