using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
            catch (Exception)
            {
                await context.Response.WriteAsync("Hello from 2nd delegate.");
            }
        }
    }

}
