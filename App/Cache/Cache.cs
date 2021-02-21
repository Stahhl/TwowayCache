using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;

namespace App.Cache
{
    public static class Cache
    {
        public static async Task<string> AddAndOrGetKeyAsync(this IDistributedCache cache, string type, string inputKey)
        {
            try
            {
                string outputKey = await cache.GetStringAsync(inputKey);

                if (string.IsNullOrEmpty(outputKey) == false)
                    return outputKey + "_cache";

                return await AddKeyToCacheAndReturn(cache, type, inputKey);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        private static async Task<string> AddKeyToCacheAndReturn(IDistributedCache cache, string type, string key)
        {
            string value = "";

            if (type == "ID")
                value = await DataAccess.GetSerialById(key);
            else if(type == "SE")
                value = await DataAccess.GetIdBySerial(key);

            await AddKeyToCache(cache, key, value);

            return value + "_db";
        }
        private static async Task AddKeyToCache(IDistributedCache cache, string key, string value)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
                throw new Exception($"Cant add to cache: key = {key}, value = {value}");

            var options = new DistributedCacheEntryOptions();
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);

            //twoway...
            await cache.SetStringAsync(key, value, options);
            await cache.SetStringAsync(value, key, options);
        }
    }
}
