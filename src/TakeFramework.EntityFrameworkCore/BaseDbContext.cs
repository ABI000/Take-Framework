using Microsoft.EntityFrameworkCore;

namespace TakeFramework.EntityFrameworkCore
{
    public abstract class BaseDbContext<TDbContext> : DbContext, IDbContextProvider<TDbContext>
        where TDbContext : DbContext
    {
        public abstract string Name { get; }

        public abstract TDbContext GetDbContext();

        public Task<TDbContext> GetDbContextAsync() => Task.FromResult(GetDbContext());
    }
}
