using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts
{
    [RequireComponent(typeof(MapDisplay))]
    public class MapGenerator : MonoBehaviour
    {
        public bool autoUpdate;
        public int mapSize;
        public float noiseScale;
        public MapDisplay mapDisplay;
        public List<Octave> octaves;
        [Range(.001f,1)] public float persistance;
        public float lacunarity;
        public int seed;
        public Vector2 offset;

        public void GenerateMap()
        {
            var noiseMap = Noise.GenerateNoiseMap(new GenerateNoiseMapOptions()
            {
                Scale = this.noiseScale,
                Size = this.mapSize,
                Persistance = this.persistance,
                Lacunarity = this.lacunarity,
                Octaves = this.octaves,
                Offset = this.offset,
                Seed = this.seed
            });
            
            this.mapDisplay.DrawNoiseMap(noiseMap);
        }

        private void OnValidate()
        {
            if (this.mapSize < 1)
            {
                this.mapSize = 1;
            }

            if (this.lacunarity < 1)
            {
                this.lacunarity = 1;
            }
        }
    }
}