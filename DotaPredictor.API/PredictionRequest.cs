namespace DotaPredictor.API;

public class PredictionRequest
{
    public IEnumerable<int> Allies { get; set; } = Array.Empty<int>();

    public IEnumerable<int> Enemies { get; set; } = Array.Empty<int>();
}
