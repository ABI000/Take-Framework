
using AutoMapper;
using AutoMapper.Execution;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TakeFramework.Cache;

namespace TakeFramework.AutoMapper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            Assembly[] assemblies = DependencyUtil.GetReferencedAssemblies();
            IEnumerable<Type> types = assemblies.SelectMany(x => x.GetTypes().Where(w => !w.IsInterface && typeof(Profile).IsAssignableFrom(w)));
            services.AddAutoMapper(cfg => { }, types);
            return services;
        }
    }
}