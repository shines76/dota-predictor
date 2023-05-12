using DotaPredictor.Client.Services;
using DotaPredictor.DataBuilder.Interfaces;
using DotaPredictor.DataBuilder.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Runtime.CompilerServices;

namespace DotaPredictor.Client
{
    public static class MauiProgram
    {
             
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif
            
            //builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddSingleton<HeroDetailsService>();
            builder.Services.AddHttpClient<HeroService>();
            builder.Services.AddSingleton<IHeroService>(p => p.GetRequiredService<HeroService>());
            builder.Services.AddSingleton<PredictorService>();
            builder.Services.AddMemoryCache();
            //register zip file and apply lifecylce events

            
            return builder.Build();
        }
        
        
    }
}