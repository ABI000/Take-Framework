﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TakeFramework.EntityFrameworkCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTakeFrameworkDbContext<TDbContext>(this IServiceCollection services, IConfiguration configuration)
            where TDbContext : DbContext
        {
            services.Configure<DBSettings>(configuration.GetSection(DBSettings.Position));
            services.AddDbContext<TDbContext>();
            services.AddScoped(typeof(IDbContextProvider<TDbContext>), typeof(TDbContext));
            return services;
        }
    }
}