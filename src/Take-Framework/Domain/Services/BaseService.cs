using TakeFramework.Domain.Uow;

namespace TakeFramework.Domain.Services
{
    /// <summary>
    /// 基础业务服务类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// 构造函数
    /// </remarks>
    /// <param name="mapper"></param>
    /// <param name="rpository"></param>
    /// <param name="httpContent"></param>
    public class BaseService(IUnitOfWork unitOfWork) : IBaseService
    {
        protected readonly IUnitOfWork _unitOfWork = unitOfWork;
    }
}
