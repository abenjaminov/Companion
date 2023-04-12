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
        private const int MapChunkSize = 241;
        
        public DrawMode drawMode;

        [Header("Mesh")] 
        public float heightMultiplier;

        public AnimationCurve meshHeightCurve;

        [Header("General")] 
        [Range(0,6)] public int LevelOfDetails;
        public bool autoUpdate;
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
                Size = MapChunkSize,
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
                HeightMultiplier = this.heightMultiplier,
                MeshHeightCurve = this.meshHeightCurve,
                LevelOfDetail = this.LevelOfDetails
            });
            var colorMap = this.GetColorMapFromRegions(noiseMap);
            mapDisplay.DrawMesh(meshData, colorMap, MapChunkSize, MapChunkSize);
        }

        private Color[] GetColorMapFromRegions(float[,] heightMap)
        {
            var colorMap = new Color[MapChunkSize * MapChunkSize];
            
            for (var y = 0; y < MapChunkSize; y++)
            {
                for (var x = 0; x < MapChunkSize; x++)
                {
                    var currentHeight = heightMap[x, y];

                    var region = this.regions.First(region => currentHeight <= region.maxHeight);

                    colorMap[y * MapChunkSize + x] = region.color;
                }
            }

            return colorMap;
        }
        
        private void DrawColorMap(float[,] noiseMap)
        {
            var colorMap = GetColorMapFromRegions(noiseMap);

            mapDisplay.DrawTexture(colorMap, MapChunkSize, MapChunkSize);
        }

        private void OnValidate()
        {
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