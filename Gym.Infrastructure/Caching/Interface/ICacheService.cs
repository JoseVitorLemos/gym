namespace Gym.Infrastructure.Caching;

public interface ICacheService
{
    Task SetAsync(string key, string value);
    Task<string> GetAsync(string key);
    void SetDistributedCacheTimes(TimeSpan absoluteExpiration, 
            TimeSpan relativeExpire);
}
