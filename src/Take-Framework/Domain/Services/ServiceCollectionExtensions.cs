using Microsoft.Extensions.DependencyInjection;

namespace TakeFramework.Domain.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            return services.ScopedInterceptorRegistrar<UowInterceptor>(typeof(IBaseService));
        }
    }
}