using TakeFramework.Domain.Uow;

namespace TakeFramework.Domain.Services
{
    /// <summary>
    /// 基础业务服务类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseService : IBaseService
    {
        protected readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="rpository"></param>
        /// <param name="httpContent"></param>
        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }




    }
}
