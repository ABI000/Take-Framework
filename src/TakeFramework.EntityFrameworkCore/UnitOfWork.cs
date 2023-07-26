using Microsoft.EntityFrameworkCore;
using TakeFramework.Domain.Uow;

namespace TakeFramework.EntityFrameworkCore
{
    public class UnitOfWork : IUnitOfWork
    //where TDbContext : DbContext
    {
        private readonly Dictionary<string, DbContext> dbContextProviders;
        public UnitOfWork(IEnumerable<IDbContextProvider> dbContextProviders)
        {
            //如何避免额外开启当前不相关数据库的事务启动
            this.dbContextProviders = dbContextProviders.ToDictionary(x => x.Name, v => (DbContext)v);

        }
        public void BeginTransaction()
        {
            foreach (var context in dbContextProviders)
            {
                context.Value.Database.BeginTransaction();
            }
        }

        public void Commit()
        {
            try
            {
                foreach (var context in dbContextProviders)
                {
                    context.Value.Database.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }

        public void Rollback()
        {
            foreach (var context in dbContextProviders)
            {
                context.Value.Database.RollbackTransaction();
            }
        }

    }
}
