using Microsoft.EntityFrameworkCore;

namespace TakeFramework.EntityFrameworkCore
{
    public interface IDbContextProvider<TDbContext>
        where TDbContext : DbContext
    {
        public TDbContext GetDbContext();

        public Task<TDbContext> GetDbContextAsync();
    }
}
