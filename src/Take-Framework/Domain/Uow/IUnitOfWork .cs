namespace TakeFramework.Domain.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        bool HasActiveTransaction { get; set; }
        void BeginTransaction(string? name = null);
        void Commit(string? name = null);
        void Rollback(string? name = null);
    }
}
