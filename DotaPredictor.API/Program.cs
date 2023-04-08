using DotaPredictor.DataBuilder.Extensions;
using DotaPredictor.DataBuilder.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDotaDataPredictor();

var app = builder.Build();
var predictor = app.Services.GetRequiredService<IPredictorService>();
//predictor.BuildModel("data.csv");
//predictor.SaveModel("model.zip");
predictor.LoadModel("model.zip");

app.MapGet("/", async (IEnumerable<int> allies, IEnumerable<int> enemies) => await predictor.PredictHeroSuccesses(allies, enemies));

app.Run();
