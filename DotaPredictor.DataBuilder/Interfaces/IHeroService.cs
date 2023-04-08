namespace DotaPredictor.DataBuilder.Interfaces;

public interface IHeroService
{
    Task<Dictionary<int, string>> GetHeroMapAsync(CancellationToken cancel = default);
}
