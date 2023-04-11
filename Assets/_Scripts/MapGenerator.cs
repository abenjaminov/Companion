using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts
{
    [RequireComponent(typeof(MapDisplay))]
    public class MapGenerator : MonoBehaviour
    {
        public DrawMode drawMode;

        [Header("Mesh")] 
        public float heightMultiplier;
        
        [Header("General")]
        public bool autoUpdate;
        public int mapSize;
        public float noiseScale;
        public MapDisplay mapDisplay;
        public int numberOfOctaves;
        [Range(.001f,1)] public float persistance;
        public float lacunarity;
        public int seed;
        public Vector2 offset;
        public List<TerrainType> regions;

        [MenuItem("MapGeneration/Generate Map %G")]
        public static void GenerateMapStatic()
        {
            var mapGenerator = FindObjectOfType<MapGenerator>();
            
            mapGenerator.GenerateMap();
        }
        
        
        public void GenerateMap()
        {
            var noiseMap = Noise.GenerateNoiseMap(new GenerateNoiseMapOptions()
            {
                Scale = this.noiseScale,
                Size = this.mapSize,
                Persistance = this.persistance,
                Lacunarity = this.lacunarity,
                NumberOfOctaves = this.numberOfOctaves,
                Offset = this.offset,
                Seed = this.seed
            });

            if (drawMode == DrawMode.ColorMap)
            {
                DrawColorMap(noiseMap);
            }
            else if(drawMode == DrawMode.NoiseMap)
            {
                mapDisplay.DrawTexture(noiseMap);    
            }
            else if (drawMode == DrawMode.Mesh)
            {
                DrawMesh(noiseMap);
            }
        }

        private void DrawMesh(float[,] noiseMap)
        {
            var meshData = MeshGenerator.GenerateTerrainMeshData(new GenerateTerrainMeshDataOptions()
            {
                HeightMap = noiseMap,
                HeightMultiplier = this.heightMultiplier
            });
            var colorMap = this.GetColorMapFromRegions(noiseMap);
            mapDisplay.DrawMesh(meshData, colorMap, this.mapSize, this.mapSize);
        }

        private Color[] GetColorMapFromRegions(float[,] heightMap)
        {
            var colorMap = new Color[this.mapSize * this.mapSize];
            
            for (var y = 0; y < this.mapSize; y++)
            {
                for (var x = 0; x < this.mapSize; x++)
                {
                    var currentHeight = heightMap[x, y];

                    var region = this.regions.First(region => currentHeight <= region.maxHeight);

                    colorMap[y * this.mapSize + x] = region.color;
                }
            }

            return colorMap;
        }
        
        private void DrawColorMap(float[,] noiseMap)
        {
            var colorMap = GetColorMapFromRegions(noiseMap);

            mapDisplay.DrawTexture(colorMap, this.mapSize, this.mapSize);
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

            var previousRegionValue = 0f;
            
            foreach (var region in this.regions)
            {
                if (region.maxHeight < previousRegionValue)
                {
                    Debug.LogError("Regions heights must be sorted");
                }

                previousRegionValue = region.maxHeight;
            }
        }
    }
}