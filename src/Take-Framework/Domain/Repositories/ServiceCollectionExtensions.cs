using Microsoft.Extensions.DependencyInjection;

namespace TakeFramework.Domain.Repositories
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddSingleton<EntityDictionary>();
            return services.ScopedInterceptorRegistrar<UowInterceptor>(typeof(IBaseRepository));
        }
    }
}