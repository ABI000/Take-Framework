


using Castle.DynamicProxy;
using TakeFramework.Domain.Uow;

namespace TakeFramework;

public class UowInterceptor(IUnitOfWork unitOfWork) : IInterceptor
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public void Intercept(IInvocation invocation)
    {
        UnitOfWorkAttribute? attribute = invocation.MethodInvocationTarget.GetCustomAttributes(typeof(UnitOfWorkAttribute), false).FirstOrDefault() as UnitOfWorkAttribute;
        if (attribute is null)
        {
            invocation.Proceed();
        }
        else
        {
            try
            {
                _unitOfWork.BeginTransaction(attribute.DBName);
                invocation.Proceed();
                _unitOfWork.CommitTransaction(attribute.DBName);
            }
            catch (System.Exception)
            {
                _unitOfWork.RollbackTransaction(attribute.DBName);
                throw;
            }

        }
    }
}
