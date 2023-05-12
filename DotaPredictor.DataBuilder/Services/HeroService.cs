using System.Text.Json;
using DotaPredictor.DataBuilder.Interfaces;
using DotaPredictor.DataBuilder.Models;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace DotaPredictor.DataBuilder.Services;

public class HeroService : IHeroService
{
    private readonly HttpClient _client;
    private readonly IMemoryCache _cache;

    public HeroService(HttpClient client, IMemoryCache cache)
    {
        _client = client;
        _cache = cache;
    }

    /// <inheritdoc />
    public async Task<Dictionary<int, string>> GetHeroMapAsync(CancellationToken cancel = default)
    {
        return await _cache.GetOrCreateAsync(
                   "dota_heroes",
                   async (entry) =>
                   {
                       entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
                       var json = await _client.GetStringAsync("https://api.opendota.com/api/heroStats", cancel);
                       Console.WriteLine(json);
                       var heroes = JsonConvert.DeserializeObject<List<HeroResponse>>(json);
                       return heroes == null ? new Dictionary<int, string>() : heroes.ToDictionary(x => x.Id, x => x.LocalizedName);
                   })
            ?? new Dictionary<int, string>();
    }
}
