using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TakeFramework.EntityFrameworkCore;

namespace SampleWeb
{
    public class SampleDbContext : DbContext, IDbContextProvider<SampleDbContext>
    {
        public SampleDbContext GetDbContext()
        {
            return this;
        }
        public Task<SampleDbContext> GetDbContextAsync()
        {
            return Task.Run(() => GetDbContext());
        }


        private readonly DBSettings _dBSettings;
        public SampleDbContext(IOptions<DBSettings> dBSettings)
        {
            _dBSettings = dBSettings.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dBSettings.ConnectionString);
        }



        public DbSet<User> User { get; set; }
    }
}
