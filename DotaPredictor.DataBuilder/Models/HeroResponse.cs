using Newtonsoft.Json;

namespace DotaPredictor.DataBuilder.Models;

public class HeroResponse
{
    public int Id { get; set; }

    [JsonProperty(PropertyName = "localized_name")]
    public string LocalizedName { get; set; } = "";
}
