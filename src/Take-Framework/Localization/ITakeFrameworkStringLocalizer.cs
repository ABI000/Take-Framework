using Microsoft.Extensions.Localization;

namespace TakeFramework.Localization
{
    public interface ITakeFrameworkStringLocalizer : IStringLocalizer
    {
        public string StorageType { get; }
    }
}
