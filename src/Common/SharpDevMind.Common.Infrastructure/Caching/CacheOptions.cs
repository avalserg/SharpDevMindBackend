using Microsoft.Extensions.Caching.Distributed;

namespace Evently.Common.Infrastructure.Caching;

public static class CacheOptions
{
    public static DistributedCacheEntryOptions DefaultExpiration => new()
    {

#pragma warning disable S1135
        //TODO setup minutes without hardcode 
#pragma warning restore S1135

        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
    };

    public static DistributedCacheEntryOptions Create(TimeSpan? expiration) =>
        expiration is not null ?
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiration } :
            DefaultExpiration;
}
