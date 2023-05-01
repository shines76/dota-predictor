using DotaPredictor.Client.Services;
using DotaPredictor.DataBuilder.Interfaces;
using DotaPredictor.DataBuilder.Services;

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
            builder.Services.AddHttpClient<HeroDetailsService>();
            builder.Services.AddSingleton<HeroDetailsService>();
            builder.Services.AddSingleton<IPredictorService, PredictorService>();
            builder.Services.AddMemoryCache();
            return builder.Build();
        }
    }
}