using BaseEntities.Response;
using Microsoft.Extensions.Caching.Memory;

namespace MemCaching.Abstract
{
    public interface ICacheService
    {
        T Get<T>(string key)
            where T : class, new();

        void AddOrUpdate(string key, object value, MemoryCacheEntryOptions options = null);

        void Delete(string key);

        void DeleteStartWithPattern(string pattern);

    }
}
