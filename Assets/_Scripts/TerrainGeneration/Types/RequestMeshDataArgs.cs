using System;

namespace _Scripts.TerrainGeneration.Types
{
    public struct RequestMeshDataArgs
    {
        public MapData MapData;
        public Action<MeshData> Callback;
        public int LevelOfDetails;
    }
}