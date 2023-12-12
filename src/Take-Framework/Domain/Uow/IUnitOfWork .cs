namespace TakeFramework.Domain.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        bool HasActiveTransaction { get; set; }
        void BeginTransaction(string? name = null);
        void CommitTransaction(string? name = null);
        void RollbackTransaction(string? name = null);
    }
}
