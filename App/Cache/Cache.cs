using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;

namespace App.Cache
{
    public static class Cache
    {
        public static async Task<string> AddAndOrGetKeyAsync(this IDistributedCache cache, KeyType type, string inputKey)
        {
            string outputKey = await cache.GetStringAsync(StringHelper.AddPrefix(type, inputKey));

            if (string.IsNullOrEmpty(outputKey) == false)
                return outputKey;

            return await AddKeyToCacheAndReturn(cache, type, inputKey);
        }
        private static async Task<string> AddKeyToCacheAndReturn(IDistributedCache cache, KeyType type, string key)
        {
            string value = type == KeyType.A ? DataAccess.GetValueForKeyA(key) : DataAccess.GetValueForKeyB(key);
            await AddKeyToCache(cache, key, value);

            return value;
        }
        private static async Task AddKeyToCache(IDistributedCache cache, string key, string value)
        {
            var options = new DistributedCacheEntryOptions();
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);

            await cache.SetStringAsync(key, value, options);
        }
    }
}
