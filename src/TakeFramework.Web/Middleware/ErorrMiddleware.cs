using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using TakeFramework.Exceptions;

namespace TakeFramework.Web.Middleware
{
    public class ErorrMiddleware(ILogger<ErorrMiddleware> logger) : IMiddleware
    {
        private readonly ILogger logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (JsonException e)
            {
                await SystemErrorAsync(context, e, "JsonError");
            }
            catch (BusinessException e)
            {
                GetErrorMsg(context, e);
                await LocalizationExceptionResponseWriteAsync(context, e.Message, e.Code);
            }
            catch (Exception ex)
            {
                await SystemErrorAsync(context, ex, "ServerErorr");

            }
        }

        private async Task SystemErrorAsync(HttpContext context, Exception ex, string code)
        {
            var msg = GetErrorMsg(context, ex);
#if DEBUG
            await ExceptionResponseWriteAsync(context, msg, code);
#else
            await LocalizationExceptionResponseWriteAsync(context, "ServerErorr", code);
#endif
        }
        private string GetErrorMsg(HttpContext context, Exception ex)
        {
            var msg = $"{context.Request.Scheme} {context.Request.Method} {context.Request.Path}{Environment.NewLine}错误信息{ex.Message}{Environment.NewLine}错误追踪:{ex.StackTrace}";
            logger.LogError(msg);
            return msg;
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
