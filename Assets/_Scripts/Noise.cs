using System.Collections.Generic;
using UnityEngine;

namespace _Scripts
{
    public struct Octave
    {
        public int DetailLevel;
    }
    public struct GenerateNoiseMapOptions
    {
        public int Size;
        public float Scale;
        public List<Octave> Octaves;
        public float Persistance;
        public float Lacunarity;
    }

    public static class Noise
    {
        public static float[,] GenerateNoiseMap(GenerateNoiseMapOptions options)
        {
            var noiseMap = new float[options.Size, options.Size];

            // We use scale so that we dont get the same values for each noise map generated
            var scale = options.Scale;

            if (scale <= 0)
            {
                scale = .0001f;
            }

            var maxNoiseHeight = float.MinValue;
            var minNoiseHeight = float.MaxValue;
            
            for (var y = 0; y < options.Size; y++)
            {
                for (var x = 0; x < options.Size; x++)
                {
                    var noiseHeight = 0f;
                    
                    foreach (var octave in options.Octaves)
                    {
                        var amplitude = Mathf.Pow(options.Persistance, octave.DetailLevel);
                        var frequency = Mathf.Pow(options.Lacunarity, octave.DetailLevel);
                        
                        var sampleX = (x / scale) * frequency;
                        var sampleY = (y / scale) * frequency;

                        var perlinValue = Mathf.Lerp(-1,1, Mathf.PerlinNoise(sampleX, sampleY));

                        noiseHeight = perlinValue * amplitude;

                    }

                    if (noiseHeight > maxNoiseHeight)
                    {
                        maxNoiseHeight = noiseHeight;
                    }
                    else if (noiseHeight < minNoiseHeight)
                    {
                        minNoiseHeight = noiseHeight;
                    }
                    
                    noiseMap[x, y] = noiseHeight;
                }
            }

            for (var y = 0; y < options.Size; y++)
            {
                for (var x = 0; x < options.Size; x++)
                {
                    noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
                }
            }

            return noiseMap;
        }
    }
}