using System;
using System.Collections.Generic;
using System.Text;

namespace OKEService.Utilities.Configurations
{
    public enum CacheProvider
    {
        MemoryCache,
        DistributedSqlServerCache,
        StackExchangeRedisCache,
        NCacheDistributedCache
    }
}
