using Microsoft.EntityFrameworkCore;
using TakeFramework.Domain.Uow;

namespace TakeFramework.EntityFrameworkCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<string, DbContext> dbContextProviders;
        private bool disposedValue;

        public UnitOfWork(IEnumerable<IDbContextProvider> dbContextProviders)
        {
            //如何避免额外开启当前不相关数据库的事务启动
            this.dbContextProviders = dbContextProviders.ToDictionary(x => x.Name, v => (DbContext)v);
        }

        public bool HasActiveTransaction { get; set; }

        public void BeginTransaction(string? name = null)
        {
            if (name is not null)
            {
                GetContext(name).Database.BeginTransaction();
            }
            else
            {
                foreach (var context in dbContextProviders)
                {
                    context.Value.Database.BeginTransaction();
                }
            }
        }

        private DbContext GetContext(string? name)
        {
            if (dbContextProviders.TryGetValue(name, out var context))
            {
                return context;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void Commit(string? name = null)
        {
            try
            {
                if (name is not null)
                {
                    GetContext(name).Database.BeginTransaction();
                }
                else
                {
                    foreach (var context in dbContextProviders)
                    {
                        context.Value.Database.CommitTransaction();
                    }
                }
            }
            catch (Exception)
            {
                Rollback(name);
                throw;
            }
        }

        public void Rollback(string? name = null)
        {
            if (name is not null)
            {
                GetContext(name).Database.BeginTransaction();
            }
            else
            {
                foreach (var context in dbContextProviders)
                {
                    context.Value.Database.RollbackTransaction();
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                }

                // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~UnitOfWork()
        // {
        //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
