using System.Runtime.Caching;

namespace Infrastructure
{
    public class MemoryCache<T> : ICache<T>
    {
        ObjectCache cache = MemoryCache.Default;
        string prefix = "Cache_Categories";

        public T Get(string key)
        {
            var obj = cache.Get(key);
            if (obj == null) return default(T);
            return (T) obj;
        }

        public void Set(string key, T value)
        {
            cache.Set(key, value, ObjectCache.InfiniteAbsoluteExpiration);
        }
	}
}
