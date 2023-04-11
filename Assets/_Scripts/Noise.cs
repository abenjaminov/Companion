using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace _Scripts
{
    [Serializable]
    public struct Octave
    {
        public int DetailsLevel;
    }
    
    public struct GenerateNoiseMapOptions
    {
        public int Size;
        public float Scale;
        public List<Octave> Octaves;
        public float Persistance;
        public float Lacunarity;
        public int Seed; // This option is so that we can get different and unique noise maps
        public Vector2 Offset; // This options enables us to scroll through the Noise
    }

    public static class Noise
    {
        public static float[,] GenerateNoiseMap(GenerateNoiseMapOptions options)
        {
            var noiseMap = new float[options.Size, options.Size];

            var prng = new Random(options.Seed);

            var octaveOffsets = new List<Vector2>();
            
            // We want each octave to be sampled from a different location on the
            // perlin noise
            foreach (var octave in options.Octaves)
            {
                var offsetX = prng.Next(-100000, 100000) + options.Offset.x;
                var offsetY = prng.Next(-100000, 100000) + options.Offset.y;
                
                octaveOffsets.Add(new Vector2(offsetX, offsetY));
            }
            
            // We use scale so that we dont get the same values for each noise map generated
            var scale = options.Scale;

            if (scale <= 0)
            {
                scale = .0001f;
            }

            var maxNoiseHeight = float.MinValue;
            var minNoiseHeight = float.MaxValue;

            var center = new Vector2(options.Size / 2f, options.Size / 2f);
            
            for (var y = 0; y < options.Size; y++)
            {
                for (var x = 0; x < options.Size; x++)
                {
                    var noiseHeight = 0f;
                    
                    for (var i = 0; i < options.Octaves.Count; i++)
                    {
                        var octave = options.Octaves[i];
                        
                        var amplitude = Mathf.Pow(options.Persistance, octave.DetailsLevel);
                        var frequency = Mathf.Pow(options.Lacunarity, octave.DetailsLevel);
                        
                        var sampleX = (x - center.x) / scale * frequency + octaveOffsets[i].x;
                        var sampleY = (y - center.y) / scale * frequency + octaveOffsets[i].y;;

                        var perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;

                        noiseHeight += perlinValue * amplitude;
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

            // Normalize all the values of the noise map to be between 0.0 and 1.0
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