using System;

namespace Cache
{
    public class Cache
    {
        public static class TwowayRedis
        {
            public static async Task<string> AddAndOrGetKeyAsync(this IDistributedCache cache, KeyType type, string inputKey)
            {
                string outputKey = await cache.GetStringAsync(StringHelper.AddPrefix(type, inputKey));

                if (string.IsNullOrEmpty(outputKey) == false)
                    return outputKey;

                return await AddKeyToCacheAndReturn(cache, type, inputKey);
            }
            public static async Task<string> AddKeyToCacheAndReturn(IDistributedCache cache, KeyType type, string key)
            {
                string value = type == KeyType.A ? DataAccess.GetValueForKeyA(key) : DataAccess.GetValueForKeyB(key);
                await AddKeyToCache(cache, key, value);

                return value;
            }
            public static async Task AddKeyToCache(IDistributedCache cache, string key, string value)
            {
                var options = new DistributedCacheEntryOptions();
                options.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);

                await cache.SetStringAsync(key, value, options);
            }

        }
        internal static class StringHelper
        {
            internal static string AddPrefix(KeyType type, string key)
            {
                return GetPrefix(type) + key;
            }
            internal static string GetPrefix(KeyType type)
            {
                return type == KeyType.A ? "a_" : "b_";
            }
        }
        internal static class DataAccess
        {
            internal static string GetValueForKeyA(string key)
            {
                return StringHelper.GetPrefix(KeyType.B) + key;
            }
            internal static string GetValueForKeyB(string key)
            {
                return StringHelper.GetPrefix(KeyType.A) + key;
            }
        }
        public enum KeyType
        {
            A,
            B
        }
    }
}
