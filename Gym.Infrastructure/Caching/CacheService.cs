using Microsoft.Extensions.Caching.Distributed;

namespace Gym.Infrastructure.Caching;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    private DistributedCacheEntryOptions _options;

    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
        _options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
            SlidingExpiration = TimeSpan.FromSeconds(1200)
        };
    }

    public void SetDistributedCacheTimes(TimeSpan absoluteExpiration,
            TimeSpan relativeExpire)
    {
        _options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = absoluteExpiration,
            SlidingExpiration = relativeExpire
        };
    }

    public async Task<string> GetAsync(string key)
        => await _cache.GetStringAsync(key);

    public async Task SetAsync(string key, string value)
        => await _cache.SetStringAsync(key, value, _options);
}
