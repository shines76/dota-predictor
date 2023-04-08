using Microsoft.ML.Data;

namespace DotaPredictor.DataBuilder.Models;

public class Match
{
    public int[] DireHeroes { get; set; }

    public int[] RadiantHeroes { get; set; }

    [VectorType(150)] public float[] DireHeroFlags { get; set; }

    [VectorType(150)] public float[] RadiantHeroFlags { get; set; }


    public bool RadiantWin { get; set; }

    public float Probability => RadiantWin ? 0.51f : 0.49f;

    public Match()
    {
        DireHeroes = Array.Empty<int>();
        RadiantHeroes = Array.Empty<int>();
    }

    public void BuildFlags()
    {
        var a = new float[150];
        foreach (var id in RadiantHeroes)
        {
            a[id] = 1;
        }

        RadiantHeroFlags = a;

        a = new float[150];
        foreach (var id in DireHeroes)
        {
            a[id] = 1;
        }

        DireHeroFlags = a;
    }
}
