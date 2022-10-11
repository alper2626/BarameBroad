using System;
using System.Collections.Generic;
using System.Linq;
using BaseEntities.Response;
using MemCaching.Abstract;
using Microsoft.Extensions.Caching.Memory;

namespace MemCaching.Concrete
{
    public class MemoryCacheManager : ICacheService
    {
        private static object _lock = new Object();
        private static IMemoryCache instance;
        private const string _cacheStorageKey = "keys";

        private static IMemoryCache _memoryCache
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        instance = new MemoryCache(new MemoryCacheOptions());
                    }
                }
                return instance;
            }
        }


        public T Get<T>(string key)
            where T : class,new()
        {
            var found = _memoryCache.TryGetValue(key, out T value);
            if (found)
                return value;

            return null;
        }

        public void DeleteStartWithPattern(string pattern)
        {
            var allKeys = ContainAllKeys(pattern);
            foreach (var key in allKeys)
                Delete(key);
        }

        public void AddOrUpdate(string key, object value, MemoryCacheEntryOptions options = null)
        {

            if (options == null)
            {
                options = new MemoryCacheEntryOptions
                {
                    Priority = CacheItemPriority.Normal,
                    SlidingExpiration = TimeSpan.FromHours(2),
                    AbsoluteExpiration = DateTime.Now.AddDays(7),
                };
            }

            if (!ContainKeyPair(key))
                AddCacheKeyPair(key);

            _memoryCache.Remove(key);
            _memoryCache.GetOrCreate(key,
                    entry =>
                    {
                        entry.SetOptions(options);
                        entry.SetValue(value);
                        return value;
                    });
        }

        public void Delete(string key)
        {
            _memoryCache.Remove(key);
        }

        private List<string> CreateOrGetCacheKeyStorage()
        {
            var foundFlag = _memoryCache.TryGetValue(_cacheStorageKey, out List<string> keys);
            if (!foundFlag)
            {
                keys = new List<string>();

                var opt = new MemoryCacheEntryOptions
                {
                    Priority = CacheItemPriority.NeverRemove,
                    SlidingExpiration = TimeSpan.FromMinutes(10),
                    AbsoluteExpiration = DateTime.Now.AddHours(2)
                };
                _memoryCache.GetOrCreate(_cacheStorageKey, entry =>
                {
                    
                    entry.SetOptions(opt);
                    entry.SetValue(keys);
                    return keys;
                });
            }


            return keys;
        }

        private void AddCacheKeyPair(string value)
        {
            var cache = CreateOrGetCacheKeyStorage();
            if (!cache.Contains(value))
                cache.Add(value);

            AddOrUpdate(_cacheStorageKey, cache);
        }

        private void DeleteCacheKeyPair(string value)
        {
            var cache = CreateOrGetCacheKeyStorage();
            if (cache.Contains(value))
                cache.Remove(value);
            AddOrUpdate(_cacheStorageKey, cache);
        }

        private bool ContainKeyPair(string value)
        {
            var cache = CreateOrGetCacheKeyStorage();
            return cache.Contains(value);
        }

        private List<string> ContainAllKeys(string keyValue)
        {
            var cache = CreateOrGetCacheKeyStorage();
            return cache.Where(q => q.Contains(keyValue)).ToList();
        }
    }
}
