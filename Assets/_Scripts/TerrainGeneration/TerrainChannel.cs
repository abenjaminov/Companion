using System;
using _Scripts.TerrainGeneration.Types;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.TerrainGeneration
{
    public static class TerrainChannel
    {
        public static UnityAction<Vector2, Action<MapData>> OnRequestMapDataEvent;
        public static UnityAction<RequestMeshDataArgs> OnRequestMeshDataEvent;
        public static UnityAction<TerrainBlock> OnUpdateTerrainBlock;

        public static void RequestMapData(Vector2 center, Action<MapData> callback)
        {
            OnRequestMapDataEvent?.Invoke(center, callback);
        }

        public static void RequestMeshData(RequestMeshDataArgs args)
        {
            OnRequestMeshDataEvent?.Invoke(args);
        }

        public static void UpdateTerrainBlock(TerrainBlock block)
        {
            OnUpdateTerrainBlock?.Invoke(block);
        }
    }
}