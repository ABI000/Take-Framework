using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;

namespace TakeFramework.DynamicProxys;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDynamicProxys(this IServiceCollection services)
    {
        services.AddSingleton(new ProxyGenerator());
        return services.ScopedRegistrar(typeof(IInterceptor));
    }
}

