using Microsoft.EntityFrameworkCore;

namespace TakeFramework.EntityFrameworkCore
{
    public interface IDbContextProvider<TDbContext> : IDbContextProvider
        where TDbContext : DbContext
    {
        public TDbContext GetDbContext();

        public Task<TDbContext> GetDbContextAsync();
    }
    public interface IDbContextProvider
    {
        public string Name { get; }
    }
}
