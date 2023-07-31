using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sample.Core;
using TakeFramework.EntityFrameworkCore;

namespace Sample.EntityFrameworkCore
{
    public class Sample2DbContext : BaseDbContext<Sample2DbContext>, IDbContextProvider
    {
        public override Sample2DbContext GetDbContext()
        {
            return this;
        }
        private readonly DBSetting _dBSettings;
        public Sample2DbContext(IOptions<DBSettings> dBSettings)
        {
            _dBSettings = dBSettings.Value.DBSettingList.FirstOrDefault(x => x.Name == Name);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dBSettings.ConnectionString);
        }

        

        public DbSet<User> User { get; set; }

        public override string Name => "Sample2";
    }
}
