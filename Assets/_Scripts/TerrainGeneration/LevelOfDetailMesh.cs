using _Scripts.TerrainGeneration.Types;
using UnityEngine;

namespace _Scripts.TerrainGeneration
{
    public class LevelOfDetailMesh
    {
        public Mesh Mesh;
        public bool HasRequestedMesh;
        public bool HasMesh;
        private int _levelOfDetail;

        private TerrainBlock _parent;
        
        public LevelOfDetailMesh(int levelOfDetail, TerrainBlock parent)
        {
            _levelOfDetail = levelOfDetail;
            _parent = parent;
        }

        public void RequestMesh(MapData mapData)
        {
            HasRequestedMesh = true;
            TerrainChannel.RequestMeshData(new RequestMeshDataArgs()
            {
                MapData = mapData,
                Callback = OnMeshDataReceived,
                LevelOfDetails = _levelOfDetail
            });
        }

        void OnMeshDataReceived(MeshData meshData)
        {
            Mesh = meshData.CreateMesh();
            HasMesh = true;

            TerrainChannel.UpdateTerrainBlock(_parent);
        }
    }
}