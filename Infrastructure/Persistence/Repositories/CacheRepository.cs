using DomainLayer.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    internal class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<string?> GetAsync(string Cachekey)
        {
            var cacheValue = await _database.StringGetAsync(Cachekey);
            return cacheValue.IsNullOrEmpty ? null : cacheValue.ToString();
        }

        public async Task SetAsync(string Cachekey, string Cachevalue, TimeSpan TimeToLive)
        {
            await _database.StringSetAsync(Cachekey, Cachevalue, TimeToLive);
        }
    }
}
