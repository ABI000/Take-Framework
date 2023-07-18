using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace TakeFramework.Web.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IServiceCollection AddErorrMiddleware(
          this IServiceCollection services)
          => services.AddScoped<ErorrMiddleware>();

        public static IApplicationBuilder UseErorrMiddleware(
            this IApplicationBuilder app)
            => app.UseMiddleware<ErorrMiddleware>();
    }
}
