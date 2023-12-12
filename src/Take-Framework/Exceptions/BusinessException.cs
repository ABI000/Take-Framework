namespace TakeFramework.Exceptions
{
    /// <summary>
    /// 业务异常
    /// 用于业务处理发生的异常处理，通常为计算错误，业务逻辑验证不通过使用
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="msg"></param>
    /// <param name="code"></param>
    public class BusinessException(string message, string code = "BusinessError") : Exception
    {
        public string Code { get; set; } = code;

        public override string Message { get; } = message;
    }
}
