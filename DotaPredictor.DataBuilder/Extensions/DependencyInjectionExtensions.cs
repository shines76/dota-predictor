using DotaPredictor.DataBuilder.Interfaces;
using DotaPredictor.DataBuilder.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DotaPredictor.DataBuilder.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDotaDataPredictor(this IServiceCollection services)
    {
        services.AddHttpClient<HeroService>();
        services.AddSingleton<IHeroService>(p => p.GetRequiredService<HeroService>());
        services.AddSingleton<IPredictorService, PredictorService>();
        services.AddMemoryCache();

        return services;
    }
}
