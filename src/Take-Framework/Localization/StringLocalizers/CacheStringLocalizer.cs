using Microsoft.Extensions.Localization;
using System.Globalization;
using TakeFramework.Cache;

namespace TakeFramework.Localization.StringLocalizers
{
    public class CacheStringLocalizer : ITakeFrameworkStringLocalizer
    {
        private readonly ICacheProvider cacheProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoafStringLocalizer"/> class.
        /// </summary>
        /// <param name="repository">公共仓储</param>
        public CacheStringLocalizer(ICacheProvider cacheProvider)
        {
            this.cacheProvider = cacheProvider;
        }

        /// <inheritdoc/>
        public LocalizedString this[string name]
        {
            get
            {
                string? value = this.GetString(name);
                return new LocalizedString(name, value ?? name, resourceNotFound: value == null);
            }
        }

        /// <inheritdoc/>
        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                string? format = this.GetString(name);
                string value = string.Format(format ?? name, arguments);
                return new LocalizedString(name, value, resourceNotFound: format == null);
            }
        }

        public string StorageType => "Cache";

        /// <summary>
        /// 获取字符串的全语言版本
        /// </summary>
        /// <param name="includeParentCultures">includeParentCultures</param>
        /// <returns>字符串的全语言列表</returns>
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            List<KeyValuePair<string, string>> localizedStrings = cacheProvider.Get<List<KeyValuePair<string, string>>>($"LocalizationResource_{cultureInfo.Name}");
            if (localizedStrings == null)
            {
                return new List<LocalizedString>();
            }
            return localizedStrings.Select(x => new LocalizedString(x.Key, x.Value, true));
        }


        private string? GetString(string name)
        {
            var localizedStrings = this.GetAllStrings(true);
            return localizedStrings?.FirstOrDefault(x => x.Name == name)?.Value;
        }
    }
}
