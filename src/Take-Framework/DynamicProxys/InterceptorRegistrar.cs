using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;

namespace TakeFramework;

public static class InterceptorRegistrar
{
    public static IServiceCollection ScopedInterceptorRegistrar<TInterceptor>(this IServiceCollection services, params Type[] objects)
     where TInterceptor : IInterceptor
    {
        var types = DependencyUtil.GetReferencedAssemblies()
                      .SelectMany(x => x.GetTypes().Where(w => objects.Any(q => q.IsAssignableFrom(w)) && w.IsClass && !w.IsAbstract));
        foreach (var type in types)
        {
            var exposedService = type.GetInterface(DependencyUtil.GetInterfaceName(type.Name));
            services.AddScoped(type);
            if (exposedService is not null)
            {
                services.AddScoped(exposedService, serviceProvider =>
                {
                    // Get an instance of the Castle Proxy Generator
                    var proxyGenerator = serviceProvider.GetRequiredService<ProxyGenerator>();
                    // Have DI build out an instance of the class that has methods
                    // you want to cache (this is a normal instance of that class 
                    // without caching added)
                    var actual = serviceProvider.GetRequiredService(type);
                    // Find all of the interceptors that have been registered, 
                    // including our caching interceptor.  (you might later add a 
                    // logging interceptor, etc.)
                    var interceptors = serviceProvider.GetServices<TInterceptor>().ToArray() as IInterceptor[];
                    // Have Castle Proxy build out a proxy object that implements 
                    // your interface, but adds a caching layer on top of the
                    // actual implementation of the class.  This proxy object is
                    // what will then get injected into the class that has a 
                    // dependency on TInterface
                    return proxyGenerator.CreateInterfaceProxyWithTarget(exposedService, actual, interceptors);
                });
            }
        }
        return services;
    }
}
