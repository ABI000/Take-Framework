using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace TakeFramework.EntityFrameworkCore
{
    public abstract class BaseDbContext<TDbContext> : DbContext, IDbContextProvider
        where TDbContext : DbContext
    {
        protected readonly DBSetting _dBSetting;
        public BaseDbContext(IOptions<DBSettings> dBSettings)
        {
            _dBSetting = dBSettings?.Value?.DBSettingList?.FirstOrDefault(x => x.Name == Name) ?? throw new AggregateException();
        }

        public virtual string Name => this.GetType().Name;

        public abstract TDbContext GetDbContext();
        public virtual Task<TDbContext> GetDbContextAsync()
        {
            return Task.FromResult(GetDbContext());
        }
    }
}
