using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TakeFramework.EntityFrameworkCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTakeFrameworkDbContext<TDbContext>(this IServiceCollection services)
            where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>();
            services.AddScoped(typeof(IDbContextProvider<TDbContext>), typeof(TDbContext));
            return services;
        }
    }
}