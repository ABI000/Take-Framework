using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.EntityFrameworkCore;
using TakeFramework.AutoMapper;
using TakeFramework.Domain.Repositories;
using TakeFramework.Domain.Services;
using TakeFramework.EntityFrameworkCore;
using TakeFramework.Web.Middleware;
using TakeFramework.Cache;

namespace Sample.Host.Shared
{
    public static class HostConfiguration
    {
        public static IServiceCollection AddHostConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCache(configuration);
            services.AddAutoMapper();
            services.AddService();
            services.AddRepository();
            services.AddTakeFrameworkDbContext<SampleDbContext>(configuration);
            services.AddErorrMiddleware();
            return services;
        }
        public static IApplicationBuilder UseHostConfiguration(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseErorrMiddleware();
            return app;
        }
    }
}
