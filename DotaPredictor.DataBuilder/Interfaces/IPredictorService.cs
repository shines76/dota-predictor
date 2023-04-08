using DotaPredictor.DataBuilder.Models;
using Microsoft.ML;

namespace DotaPredictor.DataBuilder.Interfaces;

public interface IPredictorService
{
    void BuildModel(string path);

    void LoadModel(string path);

    void SaveModel(string path);

    Task<IEnumerable<MatchPrediction>> PredictHeroSuccesses(
        IEnumerable<int> allies,
        IEnumerable<int> enemies,
        CancellationToken cancel = default);
}
