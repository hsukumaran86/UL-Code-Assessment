using Microsoft.Extensions.Caching.Memory;

namespace UL.Infrastructure.Cache
{

    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T? Get<T>(string key)
        {
            return _cache.TryGetValue(key, out T? value) ? value : default;
        }

        public void Set<T>(string key, T value, TimeSpan expiration)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            };

            _cache.Set(key, value, cacheEntryOptions);
        }

        public bool TryGetValue<T>(string key, out T? value)
        {
            return _cache.TryGetValue(key, out value);
        }
    }

}