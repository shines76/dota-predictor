using DotaPredictor.DataBuilder.Interfaces;
using DotaPredictor.DataBuilder.Models;
using Microsoft.ML;

namespace DotaPredictor.DataBuilder.Services;

public class PredictorService : IPredictorService
{
    private readonly MLContext _context;
    private readonly IHeroService _heroService;
    private ITransformer? _model;
    private DataViewSchema? _schema;

    public PredictorService(IHeroService heroService)
    {
        _heroService = heroService;
        _context = new MLContext();
    }

    /// <inheritdoc />
    public void BuildModel(string path)
    {
        var csvData = ReadMatches(path);
        var dataView = _context.Data.LoadFromEnumerable(csvData);
        _schema = dataView.Schema;

        var estimator = _context.Transforms.Concatenate("Features", nameof(Match.DireHeroFlags), nameof(Match.RadiantHeroFlags))
                                .Append(
                                     _context.BinaryClassification.Trainers.LinearSvm(
                                         labelColumnName: nameof(Match.RadiantWin),
                                         featureColumnName: "Features"));

        _model = estimator.Fit(dataView);
    }

    /// <inheritdoc />
    public void LoadModel(string path)
    {
        _model = _context.Model.Load(path, out _schema);
    }

    /// <inheritdoc />
    public void SaveModel(string path)
    {
        _context.Model.Save(_model, _schema, path);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<MatchPrediction>> PredictHeroSuccesses(
        IEnumerable<int> allies,
        IEnumerable<int> enemies,
        CancellationToken cancel = default)
    {
        var predictor = _context.Model.CreatePredictionEngine<Match, MatchPrediction>(_model);

        var heroes = await _heroService.GetHeroMapAsync(cancel);
        var results = heroes.Keys.Where(x => !allies.Contains(x) && !enemies.Contains(x))
                            .Select(
                                 id =>
                                 {
                                     var match = new Match
                                     {
                                         RadiantWin = true,
                                         DireHeroes = enemies.ToArray(),
                                         RadiantHeroes = allies.Append(id).ToArray()
                                     };

                                     match.BuildFlags();

                                     var result = predictor.Predict(match);
                                     result.HeroId = id;
                                     result.HeroName = heroes[id];
                                     return result;
                                 })
                            .OrderByDescending(x => x.Score)
                            .ToList();

        return results;
    }

    private static IEnumerable<Match> ReadMatches(string filename)
    {
        var lines = File.ReadAllText(filename).Split("\n").Where(x => !string.IsNullOrEmpty(x));

        var matches = lines.Select(
                                line =>
                                {
                                    var fields = line.Split(",");
                                    var match = new Match
                                    {
                                        DireHeroes = fields.Take(5).Select(int.Parse).ToArray(),
                                        RadiantHeroes = fields.Skip(5).Take(5).Select(int.Parse).ToArray(),
                                        RadiantWin = bool.Parse(fields[11])
                                    };

                                    match.BuildFlags();

                                    return match;
                                })
                           .ToList();

        var reverseMatches = matches.Select(
            x => new Match
            {
                RadiantHeroFlags = x.DireHeroFlags,
                DireHeroFlags = x.RadiantHeroFlags,
                RadiantWin = !x.RadiantWin
            });

        return matches.Concat(reverseMatches).ToList();
    }
}
