using Microsoft.EntityFrameworkCore;
using TakeFramework.Domain.Uow;

namespace TakeFramework.EntityFrameworkCore
{
    public class UnitOfWork(IEnumerable<IDbContextProvider> dbContextProviders) : IUnitOfWork
    {
        private readonly Dictionary<string, DbContext> dbContextProviders = dbContextProviders.ToDictionary(x => x.Name, v => (DbContext)v);
        private bool disposedValue;

        public bool HasActiveTransaction { get; set; }

        public void BeginTransaction(string? name = null)
        {
            GetContext(name).Database.BeginTransaction();
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

        public void CommitTransaction(string? name = null)
        {
            GetContext(name).Database.CommitTransaction();
        }

        public void RollbackTransaction(string? name = null)
        {
            GetContext(name).Database.RollbackTransaction();
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
        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
