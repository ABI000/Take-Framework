using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace TakeFramework.Web
{
    public static class LocalizationHelper
    {
        /// <summary>
        /// 多语言转换器
        /// </summary>
        public static IStringLocalizer Localizer { get; private set; }

        public static string L(string value)
        {
            return Localizer?[value] ?? value;
        }

        /// <summary>
        /// 加载多语言中间件
        /// </summary>
        /// <param name="app">ApplicationBuilder</param>
        public static void UseLocalization(this IApplicationBuilder app, IConfiguration configuration)
        {
            string[] languages = new string[] { "zh-cn", "en-us" };
            List<CultureInfo> supportedCultures = languages.Select(x => new CultureInfo(x)).ToList();

            RequestLocalizationOptions options = new()
            {
                DefaultRequestCulture = new RequestCulture(configuration.GetSection(Localization.LocalizationOptions.Position).GetValue<string>("DefaultLanguageCode")!),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };
            // 设置默认语言
            app.UseRequestLocalization(options);
            Localizer = app.ApplicationServices.GetRequiredService<IStringLocalizerFactory>().Create(null);
        }

    }
}
