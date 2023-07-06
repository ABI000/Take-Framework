namespace TakeFramework.Cache
{
    /// <summary>
    /// 缓存公共接口
    /// </summary>
    public interface ICacheProvider
    {
        public string Tag { get; }

        /// <summary>
        /// 连接是否通畅
        /// </summary>
        /// <returns>是否通畅</returns>
        Task<bool> ConnectionAsync();

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="key">数据的Key</param>
        /// <param name="value">数据的Value</param>
        void Add<T>(string key, T value);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <param name="value">数据的Value</param>
        /// <returns>Task</returns>
        Task AddAsync(string key, object value);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <param name="value">数据的Value</param>
        /// <param name="defaultExpire">是否使用默认过期时间</param>
        void Add(string key, object value, bool defaultExpire);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <param name="value">数据的Value</param>
        /// <param name="defaultExpire">是否使用默认过期时间</param>
        /// <returns>Task</returns>
        Task AddAsync(string key, object value, bool defaultExpire);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="key">数据的Key</param>
        /// <param name="value">数据的Value</param>
        /// <param name="numOfMinutes">过期时间</param>
        void Add<T>(string key, T value, long numOfMinutes);

        ///// <summary>
        ///// 添加数据
        ///// </summary>
        ///// <typeparam name="T">数据的类型</typeparam>
        ///// <param name="key">数据的Key</param>
        ///// <param name="value">数据的Value</param>
        ///// <param name="numOfMinutes">过期时间</param>
        //void AddAsync<T>(string key, T value, long numOfMinutes);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="key">数据的Key</param>
        /// <param name="value">数据的Value</param>
        /// <param name="numOfMinutes">过期时间</param>
        /// <returns>Task</returns>
        Task AddAsync(string key, object value, long numOfMinutes);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <param name="value">数据的Value</param>
        /// <param name="timeSpan">过期时间</param>
        void Add(string key, object value, TimeSpan timeSpan);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <param name="value">数据的Value</param>
        /// <param name="timeSpan">过期时间</param>
        /// <returns>Task</returns>
        Task AddAsync(string key, object value, TimeSpan timeSpan);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="keys">数据的Key</param>
        /// <returns>字典类型的数据</returns>
        IDictionary<string, T> Get<T>(IEnumerable<string> keys);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="keys">数据的Key</param>
        /// <returns>字典类型的数据</returns>
        Task<IDictionary<string, T>> GetAsync<T>(IEnumerable<string> keys);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <returns>数据实例</returns>
        object? Get(string key);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <returns>数据实例</returns>
        Task<object> GetAsync(string key);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="key">数据的Key</param>
        /// <returns>数据实例</returns>
        T Get<T>(string key);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="key">数据的Key</param>
        /// <returns>数据实例</returns>
        Task<T> GetAsync<T>(string key);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <param name="obj">数据实例</param>
        /// <returns>是否获取成功</returns>
        bool GetCacheTryParse(string key, out object obj);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="key">数据的Key</param>
        void Remove(string key);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <returns>Task</returns>
        Task RemoveAsync(string key);

        /// <summary>
        /// 指定key是否存在
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <returns>是否存在</returns>
        bool KeyExists(string key);

        /// <summary>
        /// 指定key是否存在
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <param name="expire">过期时间</param>
        /// <returns>Task</returns>
        Task KeyExpireAsync(string key, DateTime? expire);

        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <param name="expire">过期时间</param>
        void KeyExpire(string key, DateTime? expire);

        /// <summary>
        /// 获取指定Key剩余的过期时间
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <returns>剩余时间</returns>
        TimeSpan? KeyTimeToLive(string key);

        /// <summary>
        /// 获取指定的key的数据,如果数据不存在,执行插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="insertValueFunc"></param>
        /// <returns></returns>
        Task<T> GetOrInsertValueAsync<T>(string key, Func<Task<T>> insertValueFunc);

        #region Hash接口

        /// <summary>
        /// 获取存储在哈希表中指定字段的值
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <param name="field">数据的Field</param>
        /// <returns>字符串类型的数据</returns>
        string HGet(string key, string field);

        /// <summary>
        /// 获取存储在哈希表中指定字段的值
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="key">数据的Key</param>
        /// <param name="field">数据的Field</param>
        /// <returns>数据实例</returns>
        T HGet<T>(string key, string field);

        /// <summary>
        /// 获取存储在哈希表中指定字段的值,协程处理
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="key">数据的Key</param>
        /// <param name="field">数据的Field</param>
        /// <returns>数据实例</returns>
        Task<T> HGetAsync<T>(string key, string field);

        /// <summary>
        /// 获取在哈希表中指定 key 的所有字段和值
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="key">数据的Key</param>
        /// <returns>数据实例</returns>
        Dictionary<string, T> HGetAll<T>(string key);

        /// <summary>
        /// 获取在哈希表中指定 key 的所有字段和值，协程处理
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="key">数据的Key</param>
        /// <returns>数据实例</returns>
        Task<Dictionary<string, T>> HGetAllAsync<T>(string key);

        /// <summary>
        ///  将哈希表 key 中的字段 field 的值设为 value
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <param name="field">数据的Field</param>
        /// <param name="value">数据的Value</param>
        /// <returns>是否获取到</returns>
        bool HSet(string key, string field, object value);

        /// <summary>
        /// 批量设置哈希表
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <param name="dict">哈希表</param>
        /// <param name="expire">过期时间</param>
        void HSet(string key, Dictionary<string, string> dict, DateTime? expire = null);

        /// <summary>
        /// 批量设置哈希表
        /// </summary>
        /// <typeparam name="T">传入数据类型</param>
        /// <param name="key">数据的Key</param>
        /// <param name="dict">哈希表</param>
        /// <returns>Task</returns>
        Task HSetAsync<T>(string key, Dictionary<string, T> dict);

        /// <summary>
        /// 批量设置哈希表
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <param name="dict">哈希表</param>
        /// <param name="numOfMinutes">过期时间</param>
        /// <returns>Task</returns>
        Task HSet(string key, Dictionary<string, string> dict, long numOfMinutes);

        /// <summary>
        ///  同时将多个 field-value (域-值)对设置到哈希表 key 中
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <param name="keyValues">要设置的数据</param>
        /// <returns>是否设置成功</returns>
        bool HMSet(string key, params object[] keyValues);

        /// <summary>
        /// 获取存储在哈希表中多个字段的值
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <param name="fields">数据的fields</param>
        /// <returns>字符串类型的数据</returns>
        string[] HMGet(string key, params string[] fields);

        /// <summary>
        ///  获取所有哈希表中的字段
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <returns>获取到的fields</returns>
        string[] HKeys(string key);

        /// <summary>
        ///  删除一个或多个哈希表字段
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <param name="fields">数据的fields</param>
        /// <returns>删除掉的数量</returns>
        long HDel(string key, params string[] fields);

        /// <summary>
        /// 查看哈希表key中，指定的字段是否存在
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <param name="field">数据的Field</param>
        /// <returns>是否存在</returns>
        bool HExists(string key, string field);

        /// <summary>
        /// 获取哈希表中字段的数量
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <returns>数量</returns>
        long HLen(string key);

        /// <summary>
        /// 获取指定的key的数据,如果数据不存在,执行插入--hash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="insertValueFunc"></param>
        /// <returns></returns>
        Task<T> HGetOrInsertValueAsync<T>(string key, string field, Func<Task<T>> insertValueFunc);
        #endregion

        #region Keys接口

        /// <summary>
        /// 根据 pattern 查找符合 正则表达式的key数组
        /// </summary>
        /// <param name="pattern">正则表达式</param>
        /// <returns>key数组</returns>
        string[] Keys(string pattern);
        #endregion

        #region GEO接口（位置信息接口）

        /// <summary>
        ///  将指定的地理空间位置（纬度、经度、成员）添加到指定的key中。
        ///  这些数据将会存储到sorted set这样的目的是为了方便使用GEORADIUS或者GEORADIUSBYMEMBER命令对数据进行半径查询等操作。
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <param name="values">数据</param>
        /// <returns>long</returns>
        long GeoAdd(string key, params (decimal longitude, decimal latitude, object member)[] values);

        /// <summary>
        /// 将指定的地理空间位置（纬度、经度、成员）从指定的key中移除。
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="key">数据的Key</param>
        /// <param name="field">数据的Field</param>
        /// <returns>long</returns>
        long GeoDel<T>(string key, T field);

        /// <summary>
        /// 返回两个给定位置之间的距离。如果两个位置之间的其中一个不存在， 那么命令返回空值。
        ///  GEODIST 命令在计算距离时会假设地球为完美的球形， 在极限情况下， 这一假设最大会造成 0.5% 的误差。
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <param name="member1">第一个位置</param>
        /// <param name="member2">第二个位置</param>
        /// <param name="geoUnit">0:m，1:km,2:mi,ft:3.  m 表示单位为米；km 表示单位为千米；mi 表示单位为英里；ft 表示单位为英尺；</param>
        /// <returns>距离</returns>
        decimal? GeoDist(string key, object member1, object member2, int geoUnit = 0);

        /// <summary>
        /// 从key里返回所有给定位置元素的位置（经度和纬度）。
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <param name="members">位置</param>
        /// <returns>经度和纬度</returns>
        (decimal longitude, decimal latitude)?[] GeoPos(string key, object[] members);

        /// <summary>
        /// 以给定的经纬度为中心， 返回键包含的位置元素当中， 与中心的距离不超过给定最大距离的所有位置元素。
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">维度</param>
        /// <param name="radius">半径</param>
        /// <param name="geoUnit">0:m，1:km,2:mi,ft:3   m 表示单位为米；km 表示单位为千米；mi 表示单位为英里；ft 表示单位为英尺；</param>
        /// <param name="count"> count:使用 COUNT 选项去获取前 N 个匹配元素，</param>
        /// <param name="sorting"> 排序，0 asc,1 desc</param>
        /// <returns>位置数组</returns>
        string[] GeoRadius(string key, decimal longitude, decimal latitude, decimal radius, int geoUnit = 0, long? count = null, int? sorting = null);

        /// <summary>
        /// 以给定的成员为中心， 返回键包含的位置元素当中， 与中心的距离不超过给定最大距离的所有位置元素。
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="member">成员</param>
        /// <param name="radius">距离</param>
        /// <param name="geoUnit">0:m，1:km,2:mi,ft:3   m 表示单位为米；km 表示单位为千米；mi 表示单位为英里；ft 表示单位为英尺；</param>
        /// <param name="count">count:使用 COUNT 选项去获取前 N 个匹配元素，</param>
        /// <param name="sorting"> 排序，0 asc,1 desc</param>
        /// <returns>位置数组</returns>
        string[] GeoRadiusByMember(string key, object member, decimal radius, int geoUnit = 0, long? count = null, int? sorting = null);

        /// <summary>
        /// 以给定的经纬度为中心， 返回键包含的位置元素当中， 与中心的距离不超过给定最大距离的所有位置元素（包含距离、经度、纬度）。
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="key">数据的Key</param>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        /// <param name="radius">距离</param>
        /// <param name="geoUnit">0:m，1:km,2:mi,ft:3   m 表示单位为米；km 表示单位为千米；mi 表示单位为英里；ft 表示单位为英尺；</param>
        /// <param name="count">count:使用 COUNT 选项去获取前 N 个匹配元素，</param>
        /// <param name="sorting"> 排序，0 asc,1 desc</param>
        /// <returns>位置</returns>
        (T member, decimal dist, decimal longitude, decimal latitude)[] GeoRadiusWithDistAndCoordAsync<T>(string key, decimal longitude, decimal latitude, decimal radius, int geoUnit = 0, long? count = null, int? sorting = null);
        #endregion

        /// <summary>
        /// 设置一个Redis锁
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <param name="value">数据的Value</param>
        /// <param name="expireMS">锁释放时间(毫秒)</param>
        /// <returns>是否设置成功</returns>
        bool SetNx(string key, object value, double expireMS);

        /// <summary>
        /// 释放锁
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        void ReleaseNx(string key);

        /// <summary>
        /// 设置一个Redis锁
        /// </summary>
        /// <param name="key">数据的Key</param>
        /// <param name="value">数据的Value</param>
        /// <param name="expireMS">锁释放时间(毫秒)</param>
        /// <returns>是否设置成功</returns>
        Task<bool> SetNxAsync(string key, object value, double expireMS);

        /// <summary>
        /// 释放锁
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task ReleaseNxAsync(string key);

        #region Bit

        bool GetBit(string key, uint offset);

        Task<bool> GetBitAsync(string key, uint offset);

        void SetBit(string key, uint offset, bool value);

        Task SetBitAsync(string key, uint offset, bool value);

        /// <summary>
        /// 对一个或多个保存二进制位的字符串 key 进行位元操作，并将结果保存到 destkey 上
        /// </summary>
        /// <param name="op">And | Or | XOr | Not</param>
        /// <param name="destKey">保存结果的key</param>
        /// <param name="keys">要运算的key</param>
        /// <returns>保存到 destkey 的长度，和输入 key 中最长的长度相等</returns>
        long BitOp(string op, string destKey, string[] keys);

        /// <summary>
        /// 对一个或多个保存二进制位的字符串 key 进行位元操作，并将结果保存到 destkey 上
        /// </summary>
        /// <param name="op">And | Or | XOr | Not</param>
        /// <param name="destKey">保存结果的key</param>
        /// <param name="keys">要运算的key</param>
        /// <returns>保存到 destkey 的长度，和输入 key 中最长的长度相等</returns>
        Task<long> BitOpAsync(string op, string destKey, string[] keys);

        /// <summary>
        /// 计算给定位置被设置为 1 的比特位的数量
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="start">开始位置</param>
        /// <param name="end">结束位置</param>
        /// <returns></returns>
        long BitCount(string key, long start, long end);

        /// <summary>
        /// 计算给定位置被设置为 1 的比特位的数量
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="start">开始位置</param>
        /// <param name="end">结束位置</param>
        /// <returns></returns>
        Task<long> BitCountAsync(string key, long start, long end);

        /// <summary>
        /// 对 key 所储存的值，查找范围内第一个被设置为1或者0的bit位
        /// </summary>
        /// <returns></returns>
        long BitPos(string key, bool bit);

        /// <summary>
        /// 对 key 所储存的值，查找范围内第一个被设置为1或者0的bit位
        /// </summary>
        /// <returns></returns>
        Task<long> BitPosAsync(string key, bool bit);

        /// <summary>
        /// 将指定键的值递增1，并返回递增后的值。如果键不存在，则会创建一个新键
        /// </summary>
        /// <returns></returns>
        long IncrBy(string key, long value = 1L);

        /// <summary>
        /// 将指定键的值递增1，并返回递增后的值。如果键不存在，则会创建一个新键
        /// </summary>
        /// <returns></returns>
        Task<long> IncrByAsync(string key, long value = 1L);

        /// <summary>
        /// 为给定 key 设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        bool Expire(string key, int seconds);

        /// <summary>
        /// 为给定 key 设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        bool Expire(string key, TimeSpan expire);

        /// <summary>
        /// 给定 key 设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        Task<bool> ExpireAsync(string key, int seconds);

        /// <summary>
        /// 给定 key 设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        Task<bool> ExpireAsync(string key, TimeSpan expire);
        #endregion
    }
}
