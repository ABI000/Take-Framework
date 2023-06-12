using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TakeFramework.IO;

namespace TakeFramework.Mapper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var fileInfos = FileUtilities.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").Where(x => !x.Name.StartsWith("Microsoft.") && !x.Name.StartsWith("System."));
            services.AddAutoMapper(fileInfos.Select(x => Assembly.LoadFrom(x.FullName)));
            return services;
        }
    }
}