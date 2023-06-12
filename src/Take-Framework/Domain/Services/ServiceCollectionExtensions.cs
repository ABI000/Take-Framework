using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TakeFramework.Domain.Repositories;
using TakeFramework.IO;

namespace TakeFramework.Domain.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            var fileInfos = FileUtilities.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").Where(x => !x.Name.StartsWith("Microsoft.") && !x.Name.StartsWith("System.") && !x.Name.StartsWith("Azure."));
            var types = fileInfos.Select(x => Assembly.LoadFrom(x.FullName)).SelectMany(x => x.GetTypes().Where(w => !w.IsInterface
            && typeof(IBaseService).IsAssignableFrom(w)));
            foreach (var type in types)
            {
                services.AddScoped(type);
            }
            return services;
        }
    }
}