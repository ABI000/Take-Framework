using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TakeFramework.EntityFrameworkCore;
using Sample.Domain;


namespace Sample.EntityFrameworkCore
{
    public class SampleDbContext(IOptions<DBSettings> dBSettings) : BaseDbContext<SampleDbContext>(dBSettings), IDbContextProvider
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dBSetting.ConnectionString);
        }

        public override SampleDbContext GetDbContext()
        {
            return this;
        }
        public override string Name => "Sample";
        public DbSet<Blog> Blog { get; set; }
    }
}
