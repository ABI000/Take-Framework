using Microsoft.Extensions.DependencyInjection;

namespace TakeFramework.Domain.Managers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddManager(this IServiceCollection services)
    {
        return services.ScopedInterceptorRegistrar<UowInterceptor>(typeof(IBaseManager));
    }
}
