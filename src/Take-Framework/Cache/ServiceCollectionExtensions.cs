using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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

            return services;
        }


    }
}
