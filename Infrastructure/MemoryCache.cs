using System.Runtime.Caching;

namespace Infrastructure
{
    public class MemoryCache : ICache
    {
        readonly ObjectCache cache = System.Runtime.Caching.MemoryCache.Default;
        private readonly string prefix;

        public MemoryCache(string prefix = "")
        {
            this.prefix = prefix;
        }

        public object Get(string key)
        {
            var obj = cache.Get(prefix + key);
            return obj;
        }

        public void Set(string key, object value)
        {
            cache.Set(prefix + key, value, ObjectCache.InfiniteAbsoluteExpiration);
        }
	}
}
