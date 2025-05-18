using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace CachingLab.Services;

public class CachingService
{
    private readonly IMemoryCache _memoryCache;
    private readonly IDistributedCache _distCache;

    public CachingService(IMemoryCache memoryCache, IDistributedCache distCache)
    {
        _memoryCache = memoryCache;
        _distCache = distCache;
    }

    public async Task<string> GetOrSetAsync(string key, Func<Task<string>> factory, TimeSpan expiry)
    {
        // Сначала проверяем in-memory кеш
        if (_memoryCache.TryGetValue(key, out string memoryValue))
            return memoryValue;

        // Затем distributed кеш
        var distValue = await _distCache.GetStringAsync(key);
        if (distValue != null)
        {
            _memoryCache.Set(key, distValue, expiry);
            return distValue;
        }

        // Если нет в кешах - генерируем данные
        var value = await factory();

        // Сохраняем в оба кеша
        _memoryCache.Set(key, value, expiry);
        await _distCache.SetStringAsync(key, value, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiry
        });

        return value;
    }
}