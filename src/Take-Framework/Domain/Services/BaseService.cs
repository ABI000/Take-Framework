namespace TakeFramework.Domain.Services
{
    /// <summary>
    /// 基础业务服务类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseService : IBaseService
    {
        //protected readonly IHttpContextAccessor httpContent;
        //protected readonly BasePayLoad Payload;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="rpository"></param>
        /// <param name="httpContent"></param>
        public BaseService()
        {
            //if (httpContent != null)
            //{
            //    Payload = new BasePayLoad(httpContent.HttpContext);
            //}
            //this.httpContent = httpContent;
        }




    }
}
