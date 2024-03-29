﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TakeFramework.Cache;
using TakeFramework.Domain.Managers;
using TakeFramework.Domain.Repositories;
using TakeFramework.Domain.Services;
using TakeFramework.Domain.Uow;
using TakeFramework.Json;
using TakeFramework.JWT;
using TakeFramework.Localization;
using TakeFramework.Web.Authentication;
using TakeFramework.Web.Authorization;
using TakeFramework.Web.Middleware;
using TakeFramework.EntityFrameworkCore;
using TakeFramework.AutoMapper;
using TakeFramework.DynamicProxys;
using TakeFramework.EventBus;
using TakeFramework.EventBus.RabbitMQ;
using Sample.Server;
using TakeFramework.SemanticKernel;
namespace Sample.Host.Shared
{

    public static class HostConfiguration
    {
        public static IServiceCollection AddHostConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks();
            services.AddHttpContextAccessor();
            services.AddCache(configuration);
            services.AddAutoMapper();
            services.AddManager();
            services.AddService();
            services.AddRepository();
            services.AddTakeFrameworkDbContext<SampleDbContext>(configuration);
            services.AddErorrMiddleware();
            services.AddLocalization(configuration);

            services.AddEventBus();
            services.AddEventBusRabbitMQ(configuration);
            services.AddTransient<BlogIntegrationEventHandler>();
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
                options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());//日期时间
                options.JsonSerializerOptions.Converters.Add(new DateTimeOffsetConverter());
                options.JsonSerializerOptions.Converters.Add(new LongToStringConverter());
            });
            services.AddSemanticKernelServices(configuration);
            services.AddUnitOfWork();
            services.AddDynamicProxys();
            services.AddJwt(configuration);
            services.AddTFAuthentication();
            services.AddTFAuthorization();
            return services;
        }
        public static IApplicationBuilder UseHostConfiguration(this IApplicationBuilder app, IConfiguration configuration)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<BlogIntegrationEvent, BlogIntegrationEventHandler>();
            //app.UseLocalization(configuration);
            app.UseErorrMiddleware();
            return app;
        }
    }
}
