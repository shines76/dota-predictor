using DotaPredictor.API;
using DotaPredictor.DataBuilder.Extensions;
using DotaPredictor.DataBuilder.Interfaces;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDotaDataPredictor();

var app = builder.Build();
var predictor = app.Services.GetRequiredService<IPredictorService>();
//predictor.BuildModel("data.csv");
//predictor.SaveModel("model.zip");
predictor.LoadModel("model.zip");

app.MapPost(
    "/",
    async ([FromBody] PredictionRequest request) => await predictor.PredictHeroSuccesses(request.Allies, request.Enemies));

app.Run();
