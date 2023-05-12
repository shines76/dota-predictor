using DotaPredictor.Client.Models;
using DotaPredictor.DataBuilder.Interfaces;
using DotaPredictor.DataBuilder.Services;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace DotaPredictor.Client.Services
{
    internal class HeroDetailsService
    {

        private readonly HttpClient _client;
        private readonly IMemoryCache _cache;

        public HeroDetailsService(HttpClient client, IMemoryCache cache)
        {
            _client = client;
            _cache = cache;
        }

        public async Task<List<Hero>> GetHeroListAsync(CancellationToken cancel = default)
        {
            return await _cache.GetOrCreateAsync(
                       "dota_heroes2",
                       async (entry) =>
                       {
                           entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
                           var json = await _client.GetStringAsync("https://api.opendota.com/api/heroStats", cancel);
                           var heroes = JsonSerializer.Deserialize<List<Hero>>(json);
                           return heroes == null ? new List<Hero>() : heroes.ToList();
                       })
                ?? new List<Hero>();
        }

       
    }
}

