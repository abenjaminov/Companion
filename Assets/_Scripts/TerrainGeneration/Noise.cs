using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace _Scripts.TerrainGeneration
{
    public struct GenerateNoiseMapOptions
    {
        public int Size;
        public float Scale;
        public int NumberOfOctaves;
        public float Persistance;
        public float Lacunarity;
        public int Seed; // This option is so that we can get different and unique noise maps
        public Vector2 Offset; // This options enables us to scroll through the Noise
    }

    public static class Noise
    {
        static float _minNoiseHeight = float.MaxValue;
        static float _maxNoiseHeight = float.MinValue;
        
        public static float[,] GenerateNoiseMap(GenerateNoiseMapOptions options)
        {
            var noiseMap = new float[options.Size, options.Size];

            var prng = new Random(options.Seed);

            Debug.Log(_minNoiseHeight + " " + _maxNoiseHeight);

            var octaveOffsets = new List<Vector2>();
            
            // We want each octave to be sampled from a different location on the
            // perlin noise
            for(int i = 0; i < options.NumberOfOctaves; i++)
            {
                var offsetX = prng.Next(-100000, 100000) + options.Offset.x;
                var offsetY = prng.Next(-100000, 100000) - options.Offset.y;
                
                octaveOffsets.Add(new Vector2(offsetX, offsetY));
            }
            
            // We use scale so that we dont get the same values for each noise map generated
            var scale = options.Scale;

            if (scale <= 0)
            {
                scale = .0001f;
            }

            var center = new Vector2(options.Size / 2f, options.Size / 2f);
            
            for (var y = 0; y < options.Size; y++)
            {
                for (var x = 0; x < options.Size; x++)
                {
                    var noiseHeight = 0f;
                    var amplitude = 1f;
                    var frequency = 1f;
                    
                    for (var i = 0; i < options.NumberOfOctaves; i++)
                    {
                        var sampleX = (x - center.x + octaveOffsets[i].x) / scale * frequency;
                        var sampleY = (y - center.y + octaveOffsets[i].y) / scale * frequency;

                        var perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;

                        noiseHeight += perlinValue * amplitude;

                        amplitude *= options.Persistance;
                        frequency *= options.Lacunarity;
                    }

                    if (noiseHeight > _maxNoiseHeight)
                    {
                        _maxNoiseHeight = noiseHeight;
                    }
                    else if (noiseHeight < _minNoiseHeight)
                    {
                        _minNoiseHeight = noiseHeight;
                    }
                    
                    noiseMap[x, y] = noiseHeight;
                }
            }

            // Normalize all the values of the noise map to be between 0.0 and 1.0
            for (var y = 0; y < options.Size; y++)
            {
                for (var x = 0; x < options.Size; x++)
                {
                    noiseMap[x, y] = Mathf.InverseLerp(_minNoiseHeight, _maxNoiseHeight, noiseMap[x, y]);
                }
            }

            return noiseMap;
        }
    }
}