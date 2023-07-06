using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace TakeFramework.Localization
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLocalizationExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            var types = DependencyUtil.GetReferencedAssemblies().SelectMany(x => x.GetTypes().Where(w => !w.IsInterface && typeof(ITakeFrameworkStringLocalizer).IsAssignableFrom(w)));
            foreach (var type in types)
            {
                services.AddScoped(type);
            }
            services.Configure<LocalizationOptions>(configuration.GetSection(LocalizationOptions.Position));
            services.AddSingleton<IStringLocalizerFactory, StringLocalizerFactory>();
            return services;
        }
    }
}
