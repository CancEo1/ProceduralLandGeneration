using UnityEngine;

[CreateAssetMenu()]
public class NoiseData : ScriptableObject
{
    public Noise.NormalizeMode normalizeMode;

    public float noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;


}
