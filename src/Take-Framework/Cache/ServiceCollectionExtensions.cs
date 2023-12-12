using Castle.DynamicProxy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TakeFramework.Domain.Repositories;
using TakeFramework.Domain.Services;

namespace TakeFramework.Cache
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configuration)
        {
            IEnumerable<Assembly> assemblies = DependencyUtil.GetReferencedAssemblies();

            var types = assemblies.SelectMany(x => x.GetTypes().Where(w => !w.IsInterface && typeof(ICacheProvider).IsAssignableFrom(w)));
            foreach (var type in types)
            {
                services.AddSingleton(typeof(ICacheProvider), type);
            }
            services.AddSingleton<CacheProviderFactory>();

            services.AddScoped(typeof(IBaseRepository), serviceProvider =>
            {
                // Get an instance of the Castle Proxy Generator
                var proxyGenerator = serviceProvider.GetRequiredService<ProxyGenerator>();
                // Have DI build out an instance of the class that has methods
                // you want to cache (this is a normal instance of that class 
                // without caching added)
                var actual = serviceProvider.GetRequiredService<IBaseRepository>();
                // Find all of the interceptors that have been registered, 
                // including our caching interceptor.  (you might later add a 
                // logging interceptor, etc.)
                var interceptors = serviceProvider.GetServices<UowInterceptor>().ToArray();
                // Have Castle Proxy build out a proxy object that implements 
                // your interface, but adds a caching layer on top of the
                // actual implementation of the class.  This proxy object is
                // what will then get injected into the class that has a 
                // dependency on TInterface
                return proxyGenerator.CreateInterfaceProxyWithTarget(typeof(IBaseRepository), actual, interceptors);
            });
            services.AddScoped(typeof(IBaseService), serviceProvider =>
            {
                // Get an instance of the Castle Proxy Generator
                var proxyGenerator = serviceProvider.GetRequiredService<ProxyGenerator>();
                // Have DI build out an instance of the class that has methods
                // you want to cache (this is a normal instance of that class 
                // without caching added)
                var actual = serviceProvider.GetRequiredService<IBaseService>();
                // Find all of the interceptors that have been registered, 
                // including our caching interceptor.  (you might later add a 
                // logging interceptor, etc.)
                var interceptors = serviceProvider.GetServices<UowInterceptor>().ToArray();
                // Have Castle Proxy build out a proxy object that implements 
                // your interface, but adds a caching layer on top of the
                // actual implementation of the class.  This proxy object is
                // what will then get injected into the class that has a 
                // dependency on TInterface
                return proxyGenerator.CreateInterfaceProxyWithTarget(typeof(IBaseService), actual, interceptors);
            });
            return services;
        }


    }
}
