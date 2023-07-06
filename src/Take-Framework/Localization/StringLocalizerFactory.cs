using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace TakeFramework.Localization
{
    public class StringLocalizerFactory : IStringLocalizerFactory
    {

        protected readonly IStringLocalizer _localizedStrings;

        /// <summary>
        /// 
        /// </summary>
        public StringLocalizerFactory(IEnumerable<ITakeFrameworkStringLocalizer> descriptors, IOptions<LocalizationOptions> options)
        {
            ITakeFrameworkStringLocalizer? takeFrameworkStringLocalizer = descriptors.FirstOrDefault(x => x.Tag.Equals(options.Value.StorageType)) ?? throw new ArgumentNullException(nameof(IStringLocalizer));
            _localizedStrings = takeFrameworkStringLocalizer;
        }



        public IStringLocalizer Create(Type resourceSource)
        {
            return _localizedStrings;
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return _localizedStrings;
        }
    }


}
