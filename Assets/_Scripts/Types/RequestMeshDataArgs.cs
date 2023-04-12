using System;

namespace _Scripts.Types
{
    public struct RequestMeshDataArgs
    {
        public MapData MapData;
        public Action<MeshData> Callback;
        public int LevelOfDetails;
    }
}