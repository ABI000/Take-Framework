using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.EntityFrameworkCore;
using TakeFramework.AutoMapper;
using TakeFramework.Domain.Repositories;
using TakeFramework.Domain.Services;
using TakeFramework.EntityFrameworkCore;

namespace Sample.Host.Shared
{
    public static class HostConfiguration
    {
        public static IServiceCollection AddHostConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper();
            services.AddService();
            services.AddRepository();
            services.AddTakeFrameworkDbContext<SampleDbContext>(configuration);
            return services;
        }
        public static IApplicationBuilder UseHostConfiguration(this IApplicationBuilder app, IConfiguration configuration)
        {
            return app;
        }
    }
}
