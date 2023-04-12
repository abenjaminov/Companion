using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts
{
    [RequireComponent(typeof(MapDisplay))]
    public class MapGenerator : MonoBehaviour
    {
        public const int TerrainBlockSize = 241;
        
        public DrawMode drawMode;

        [Header("Mesh")] 
        public float heightMultiplier;

        public AnimationCurve meshHeightCurve;

        [Header("General")]
        [Range(0,6)] public int levelOfDetails;
        public bool autoUpdate;
        public float noiseScale;
        public MapDisplay mapDisplay;
        public int numberOfOctaves;
        [Range(.001f,1)] public float persistance;
        public float lacunarity;
        public int seed;
        public Vector2 offset;
        public List<TerrainType> regions;

        private Queue<MapThreadInfo<MapData>> mapThreadInfoQueue = new();
        private Queue<MapThreadInfo<MeshData>> meshThreadInfoQueue = new();

        private void Start()
        {
            TerrainChannel.OnRequestMapDataEvent += RequestMapData;
            TerrainChannel.OnRequestMeshDataEvent += RequestMeshData;
        }

        [MenuItem("MapGeneration/Generate Map %G")]
        public static void GenerateMapStatic()
        {
            var mapGenerator = FindObjectOfType<MapGenerator>();
            
            mapGenerator.GenerateMapData();
        }

        public void RequestMapData(Action<MapData> callback)
        {
            var threadStart = new ThreadStart(() =>
            {
                MapDataThread(callback);
            });

            new Thread(threadStart).Start();
        }

        private void MapDataThread(Action<MapData> callback)
        {
            var mapData = GenerateMapData();
            
            lock (mapThreadInfoQueue)
            {
                mapThreadInfoQueue.Enqueue(new MapThreadInfo<MapData>(mapData, callback));    
            }
        }
        
        public void RequestMeshData(MapData mapData, Action<MeshData> callback)
        {
            var threadStart = new ThreadStart(() =>
            {
                MeshDataThread(mapData, callback);
            });

            new Thread(threadStart).Start();
        }

        private void MeshDataThread(MapData mapData, Action<MeshData> callback)
        {
            var meshData = MeshGenerator.GenerateTerrainMeshData(new GenerateTerrainMeshDataOptions()
            {
                HeightMap = mapData.heightMap,
                HeightMultiplier = heightMultiplier,
                MeshHeightCurve = meshHeightCurve,
                LevelOfDetail = levelOfDetails
            });
            
            lock (meshThreadInfoQueue)
            {
                meshThreadInfoQueue.Enqueue(new MapThreadInfo<MeshData>(meshData, callback));    
            }
        }

        private void Update()
        {
            CallMapDataCallbacks();
            CallMeshDataCallbacks();
        }

        void CallMapDataCallbacks()
        {
            lock (mapThreadInfoQueue)
            {
                if (mapThreadInfoQueue.Count == 0) return;

                for (int i = 0; i < mapThreadInfoQueue.Count; i++)
                {
                    var threadInfo = mapThreadInfoQueue.Dequeue();
                    threadInfo.Callback(threadInfo.Parameter);
                }
            }
        }
        
        void CallMeshDataCallbacks()
        {
            lock (meshThreadInfoQueue)
            {
                if (meshThreadInfoQueue.Count == 0) return;

                for (int i = 0; i < meshThreadInfoQueue.Count; i++)
                {
                    var threadInfo = meshThreadInfoQueue.Dequeue();
                    threadInfo.Callback(threadInfo.Parameter);
                }
            }
        }

        public MapData GenerateMapData()
        {
            var heightMap = Noise.GenerateNoiseMap(new GenerateNoiseMapOptions()
            {
                Scale = this.noiseScale,
                Size = TerrainBlockSize,
                Persistance = this.persistance,
                Lacunarity = this.lacunarity,
                NumberOfOctaves = this.numberOfOctaves,
                Offset = this.offset,
                Seed = this.seed
            });

            var colorMap = this.GetColorMapFromRegions(heightMap);

            return new MapData()
            {
                heightMap = heightMap,
                colorMap = colorMap
            };
        }

        public void DrawMapInEditor()
        {
            var mapData = this.GenerateMapData();
            if (drawMode == DrawMode.ColorMap)
            {
                DrawColorMap(mapData);
            }
            else if(drawMode == DrawMode.NoiseMap)
            {
                mapDisplay.DrawTexture(mapData.heightMap);    
            }
            else if (drawMode == DrawMode.Mesh)
            {
                DrawMesh(mapData);
            }
        }
        
        private void DrawMesh(MapData mapData)
        {
            var meshData = MeshGenerator.GenerateTerrainMeshData(new GenerateTerrainMeshDataOptions()
            {
                HeightMap = mapData.heightMap,
                HeightMultiplier = this.heightMultiplier,
                MeshHeightCurve = this.meshHeightCurve,
                LevelOfDetail = this.levelOfDetails
            });
            mapDisplay.DrawMesh(meshData, mapData.colorMap, TerrainBlockSize, TerrainBlockSize);
        }

        private Color[] GetColorMapFromRegions(float[,] heightMap)
        {
            var colorKeys = new List<GradientColorKey>(regions.Count);

            for (var i = 0; i < regions.Count; i++)
            {
                colorKeys.Add(new GradientColorKey(regions[i].color, regions[i].maxHeight));
            }
            
            var gradiant = new Gradient()
            {
                colorKeys = colorKeys.ToArray()
            };
            
            var colorMap = new Color[TerrainBlockSize * TerrainBlockSize];
            
            for (var y = 0; y < TerrainBlockSize; y++)
            {
                for (var x = 0; x < TerrainBlockSize; x++)
                {
                    var currentHeight = heightMap[x, y];

                    // var region = this.regions.First(region => currentHeight <= region.maxHeight);

                    colorMap[y * TerrainBlockSize + x] = gradiant.Evaluate(currentHeight);
                }
            }

            return colorMap;
        }
        
        private void DrawColorMap(MapData mapData)
        {
            mapDisplay.DrawTexture(mapData.colorMap, TerrainBlockSize, TerrainBlockSize);
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