using Microsoft.Extensions.DependencyInjection;
using TakeFramework.AutoMapper;

namespace TakeFramework.AutoMapper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(DependencyUtil.GetReferencedAssemblies());
            return services;
        }
    }
}