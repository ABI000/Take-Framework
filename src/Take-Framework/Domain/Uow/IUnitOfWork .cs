namespace TakeFramework.Domain.Uow
{
    public interface IUnitOfWork//: IDisposable
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        //void SaveChanges();
    }
}
