// using Microsoft.AspNetCore.Builder;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using NJsonSchema;
// using NJsonSchema.Generation.TypeMappers;
// using NSwag;

// namespace TakeFramework.Swagger
// {
//     public static class ServiceCollectionExtensions
//     {
//         public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
//         {
//             // services.AddOpenApiDocument(config =>
//             // {
//             //     config.UseControllerSummaryAsTagDescription = true;
//             //     config.AddSecurity("JwtBearer", Enumerable.Empty<string>(), new OpenApiSecurityScheme()
//             //     {
//             //         Description = "这是方式一(直接在输入框中输入认证信息，不需要在开头添加Bearer)",
//             //         Name = "Authorization",//jwt默认的参数名称
//             //         In = OpenApiSecurityApiKeyLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
//             //         Type = OpenApiSecuritySchemeType.Http,
//             //         Scheme = "bearer"
//             //     });
//             //     config.TypeMappers.Add(new ObjectTypeMapper(typeof(long), new JsonSchema
//             //     {
//             //         Type = JsonObjectType.String
//             //     }));
//             //     config.PostProcess = document =>
//             //     {
//             //         document.DocumentPath = Environment.CurrentDirectory;
//             //     };
//             // });
//             return services;
//         }
//         public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IConfiguration configuration)
//         {
//             // app.UseOpenApi();
//             // app.UseSwaggerUi3();
//             return app;
//         }

//     }
// }