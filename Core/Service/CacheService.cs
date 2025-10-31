using DomainLayer.Contracts;
using ServiceAbstration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service
{
    internal class CacheService(ICacheRepository cacheRepository) : ICacheService
    {
        public async Task<string?> GetAsync(string cachekey)
        {
            return await cacheRepository.GetAsync(cachekey);
        }

        public async Task SetAsync(string cachekey, string cachevalue, TimeSpan timeToLive)
        {
            var value = JsonSerializer.Serialize(cachevalue);
            await cacheRepository.SetAsync(cachekey, value, timeToLive);
        }
    }
}
