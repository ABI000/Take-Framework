namespace TakeFramework.Web
{
    /// <summary>
    /// 响应实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T>(T? data) : ApiResponse
    {
        /// <summary>
        /// 数据主体
        /// </summary>
        public T? Data { get; set; } = data;
    }
    public class ApiResponse
    {
        public ApiResponse() { }
        public ApiResponse(string msg, string code)
        {
            this.Msg = msg;
            this.Code = code;
        }
        /// <summary>
        /// 报错信息
        /// </summary>
        public string? Msg { get; set; }
        /// <summary>
        /// 错误编码
        /// </summary>
        public string Code { get; set; } = "0";
    }

}
