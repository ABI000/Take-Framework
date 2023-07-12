using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using TakeFramework.Exceptions;
using TakeFramework.Web;
using TakeFramework.Localization;
namespace TakeFramework.Middleware
{
    public class ErorrMiddleware : IMiddleware
    {
        private readonly ILogger logger;
        public ErorrMiddleware(ILogger<ErorrMiddleware> logger)
        {
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (BusinessException e)
            {
                await LocalizationExceptionResponseWriteAsync(context, e.Msg, e.Code);
            }
            catch (Exception ex)
            {
                var msg = $"{context.Request.Scheme} {context.Request.Method} {context.Request.Path}{Environment.NewLine}错误信息{ex.Message}{Environment.NewLine}错误追踪:{ex.StackTrace}";
                logger.LogError(msg);
#if DEBUG
                await ExceptionResponseWriteAsync(context, msg, "ServerErorr");
#else
                await LocalizationExceptionResponseWriteAsync(context, "ServerErorr", "ServerErorr");
#endif

            }
        }
        /// <summary>
        /// 本地化报错返回
        /// </summary>
        /// <param name="context"></param>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task LocalizationExceptionResponseWriteAsync(HttpContext context, string msg, string code)
        {
            await ExceptionResponseWriteAsync(context, LocalizationHelper.L(msg), code);
        }
        /// <summary>
        /// 报错返回
        /// </summary>
        /// <param name="context"></param>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task ExceptionResponseWriteAsync(HttpContext context, string msg, string code)
        {
            context.Response.ContentType = "application/json;charset=utf-8";
            context.Response.StatusCode = 500;
            await context.Response.Body.WriteAsync(GetApiResponse(msg, code));
        }
        private byte[] GetApiResponse(string msg, string code)
        {
            return JsonSerializer.SerializeToUtf8Bytes(new ApiResponse(msg, code));
        }
    }

}
