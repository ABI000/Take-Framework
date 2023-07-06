using Microsoft.Extensions.DependencyInjection;

namespace TakeFramework.Cache
{
    public class CacheProviderFactory
    {
        protected readonly Dictionary<string, ICacheProvider> keyValuePairs;

        public CacheProviderFactory(IEnumerable<ICacheProvider> cacheProviders)
        {
            keyValuePairs = cacheProviders.ToDictionary(x => x.Tag);         
        }

        public ICacheProvider GetCacheProviderByKey(string key)
        {
            return keyValuePairs[key];
        }
        public ICacheProvider GetCacheProvider()
        {
            return GetCacheProviderByKey("Cache");
        }
    }
}
