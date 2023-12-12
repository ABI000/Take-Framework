namespace TakeFramework.Cache
{
    public class CacheProviderFactory(IEnumerable<ICacheProvider> cacheProviders)
    {
        protected readonly Dictionary<string, ICacheProvider> keyValuePairs = cacheProviders.ToDictionary(x => x.Tag);

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
