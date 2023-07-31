using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sample.Core;
using TakeFramework.EntityFrameworkCore;

namespace Sample.EntityFrameworkCore
{
    public class SampleDbContext : BaseDbContext<SampleDbContext>, IDbContextProvider
    {
        private readonly DBSetting _dBSettings;
        public SampleDbContext(IOptions<DBSettings> dBSettings)
        {
            _dBSettings = dBSettings?.Value?.DBSettingList?.FirstOrDefault(x => x.Name == Name);
            if (_dBSettings is null)
            {
                throw new AggregateException();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dBSettings.ConnectionString);
        }

        public override SampleDbContext GetDbContext()
        {
            return this;
        }

        public DbSet<User> User { get; set; }

        public override string Name => "Sample";
    }
}
