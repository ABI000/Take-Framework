using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using TakeFramework.Exceptions;
using TakeFramework.Web;

namespace TakeFramework.Middleware
{
    public class ErorrMiddleware : IMiddleware
    {

        public ErorrMiddleware()
        {

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
            catch (Exception e)
            {
                //if (1 == 1)
                //{

                //    await LocalizationExceptionResponseWriteAsync(context, "ServerErorr", "ServerErorr");
                //}
                //else
                //{
                await ExceptionResponseWriteAsync(context, e.Message, "ServerErorr");
                //}
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
            await ExceptionResponseWriteAsync(context, msg, code);
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
            await context.Response.Body.WriteAsync(JsonSerializer.SerializeToUtf8Bytes(new ApiResponse(msg, code)));
        }

    }

}
