using Microsoft.ML.Data;

namespace DotaPredictor.DataBuilder.Models;

public class MatchPrediction
{
    [ColumnName("PredictedLabel")]
    public bool Prediction { get; set; }

    public float Probability { get; set; }

    public float Score { get; set; }

    public int HeroId { get; set; }
    
    public string? HeroName { get; set; }
}
