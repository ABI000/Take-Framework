using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeFramework.Cache
{
    public class CacheProvider(IMemoryCache memoryCache) : ICacheProvider
    {
        public string Tag => "Cache";
        private readonly IMemoryCache memoryCache = memoryCache;

        #region add

        public void Add<T>(string key, T value)
        {
            memoryCache.Set(key, value);
        }

        public void Add(string key, object value, bool defaultExpire)
        {
            throw new NotImplementedException();
        }

        public void Add<T>(string key, T value, long numOfMinutes)
        {
            throw new NotImplementedException();
        }

        public void Add(string key, object value, TimeSpan timeSpan)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(string key, object value)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(string key, object value, bool defaultExpire)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(string key, object value, long numOfMinutes)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(string key, object value, TimeSpan timeSpan)
        {
            throw new NotImplementedException();
        }

        #endregion
        public long BitCount(string key, long start, long end)
        {
            throw new NotImplementedException();
        }

        public Task<long> BitCountAsync(string key, long start, long end)
        {
            throw new NotImplementedException();
        }

        public long BitOp(string op, string destKey, string[] keys)
        {
            throw new NotImplementedException();
        }

        public Task<long> BitOpAsync(string op, string destKey, string[] keys)
        {
            throw new NotImplementedException();
        }

        public long BitPos(string key, bool bit)
        {
            throw new NotImplementedException();
        }

        public Task<long> BitPosAsync(string key, bool bit)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ConnectionAsync()
        {
            throw new NotImplementedException();
        }

        public bool Expire(string key, int seconds)
        {
            throw new NotImplementedException();
        }

        public bool Expire(string key, TimeSpan expire)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExpireAsync(string key, int seconds)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExpireAsync(string key, TimeSpan expire)
        {
            throw new NotImplementedException();
        }

        public long GeoAdd(string key, params (decimal longitude, decimal latitude, object member)[] values)
        {
            throw new NotImplementedException();
        }

        public long GeoDel<T>(string key, T field)
        {
            throw new NotImplementedException();
        }

        public decimal? GeoDist(string key, object member1, object member2, int geoUnit = 0)
        {
            throw new NotImplementedException();
        }

        public (decimal longitude, decimal latitude)?[] GeoPos(string key, object[] members)
        {
            throw new NotImplementedException();
        }

        public string[] GeoRadius(string key, decimal longitude, decimal latitude, decimal radius, int geoUnit = 0, long? count = null, int? sorting = null)
        {
            throw new NotImplementedException();
        }

        public string[] GeoRadiusByMember(string key, object member, decimal radius, int geoUnit = 0, long? count = null, int? sorting = null)
        {
            throw new NotImplementedException();
        }

        public (T member, decimal dist, decimal longitude, decimal latitude)[] GeoRadiusWithDistAndCoordAsync<T>(string key, decimal longitude, decimal latitude, decimal radius, int geoUnit = 0, long? count = null, int? sorting = null)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, T> Get<T>(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }

        public object? Get(string key)
        {
            return memoryCache.Get(key);
        }

        public T Get<T>(string key)
        {
            return (T)memoryCache.Get(key);
        }

        public Task<IDictionary<string, T>> GetAsync<T>(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        public bool GetBit(string key, uint offset)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetBitAsync(string key, uint offset)
        {
            throw new NotImplementedException();
        }

        public bool GetCacheTryParse(string key, out object obj)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetOrInsertValueAsync<T>(string key, Func<Task<T>> insertValueFunc)
        {
            throw new NotImplementedException();
        }

        public long HDel(string key, params string[] fields)
        {
            throw new NotImplementedException();
        }

        public bool HExists(string key, string field)
        {
            throw new NotImplementedException();
        }

        public string HGet(string key, string field)
        {
            throw new NotImplementedException();
        }

        public T HGet<T>(string key, string field)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, T> HGetAll<T>(string key)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<string, T>> HGetAllAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        public Task<T> HGetAsync<T>(string key, string field)
        {
            throw new NotImplementedException();
        }

        public Task<T> HGetOrInsertValueAsync<T>(string key, string field, Func<Task<T>> insertValueFunc)
        {
            throw new NotImplementedException();
        }

        public string[] HKeys(string key)
        {
            throw new NotImplementedException();
        }

        public long HLen(string key)
        {
            throw new NotImplementedException();
        }

        public string[] HMGet(string key, params string[] fields)
        {
            throw new NotImplementedException();
        }

        public bool HMSet(string key, params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public bool HSet(string key, string field, object value)
        {
            throw new NotImplementedException();
        }

        public void HSet(string key, Dictionary<string, string> dict, DateTime? expire = null)
        {
            throw new NotImplementedException();
        }

        public Task HSet(string key, Dictionary<string, string> dict, long numOfMinutes)
        {
            throw new NotImplementedException();
        }

        public Task HSetAsync<T>(string key, Dictionary<string, T> dict)
        {
            throw new NotImplementedException();
        }

        public long IncrBy(string key, long value = 1)
        {
            throw new NotImplementedException();
        }

        public Task<long> IncrByAsync(string key, long value = 1)
        {
            throw new NotImplementedException();
        }

        public bool KeyExists(string key)
        {
            throw new NotImplementedException();
        }

        public void KeyExpire(string key, DateTime? expire)
        {
            throw new NotImplementedException();
        }

        public Task KeyExpireAsync(string key, DateTime? expire)
        {
            throw new NotImplementedException();
        }

        public string[] Keys(string pattern)
        {
            throw new NotImplementedException();
        }

        public TimeSpan? KeyTimeToLive(string key)
        {
            throw new NotImplementedException();
        }


        public void ReleaseNx(string key)
        {
            throw new NotImplementedException();
        }

        public Task ReleaseNxAsync(string key)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }

        public void SetBit(string key, uint offset, bool value)
        {
            throw new NotImplementedException();
        }

        public Task SetBitAsync(string key, uint offset, bool value)
        {
            throw new NotImplementedException();
        }

        public bool SetNx(string key, object value, double expireMS)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetNxAsync(string key, object value, double expireMS)
        {
            throw new NotImplementedException();
        }
    }
}
