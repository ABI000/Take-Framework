using Microsoft.Extensions.Localization;

namespace TakeFramework.Localization.StringLocalizers
{
    public class FileStringLocalizer : ITakeFrameworkStringLocalizer
    {
        public LocalizedString this[string name] => throw new NotImplementedException();

        public LocalizedString this[string name, params object[] arguments] => throw new NotImplementedException();

        public string StorageType => "File";

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }
    }
}
