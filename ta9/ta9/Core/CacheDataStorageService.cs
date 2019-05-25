using System.Runtime.Caching;
using ta9.Core.interfaces;

namespace ta9.Core
{
    public class CacheDataStorageService<T> : IDataStorageService<T>
    {
        public T Get(string key)
        {
            MemoryCache memoryCache = MemoryCache.Default;

            var cacheObj = memoryCache.Get(key);

            if (cacheObj == null) return default(T);

            return (T)memoryCache.Get(key);
        }

        public void Remove(string key)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(key)) memoryCache.Remove(key);
        }

        public void Save(string key, T entity)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            memoryCache.Set(key, entity, new CacheItemPolicy());
        }
    }
}