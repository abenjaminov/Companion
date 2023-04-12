using System;
using UnityEngine.Events;

namespace _Scripts
{
    public static class TerrainChannel
    {
        public static UnityAction<Action<MapData>> OnRequestMapDataEvent;
        public static UnityAction<MapData, Action<MeshData>> OnRequestMeshDataEvent;

        public static void RequestMapData(Action<MapData> callback)
        {
            OnRequestMapDataEvent?.Invoke(callback);
        }

        public static void RequestMeshData(MapData mapData, Action<MeshData> callback)
        {
            OnRequestMeshDataEvent?.Invoke(mapData, callback);
        }
    }
}