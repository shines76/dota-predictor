using DotaPredictor.DataBuilder.Extensions;
using DotaPredictor.DataBuilder.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDotaDataPredictor();

var app = builder.Build();
var predictor = app.Services.GetRequiredService<IPredictorService>();
predictor.BuildModel("data.csv");
predictor.SaveModel("model.zip");

app.MapGet("/", () => "Hello World!");

app.Run();
