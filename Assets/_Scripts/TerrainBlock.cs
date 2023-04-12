using UnityEngine;

namespace _Scripts
{
    public class TerrainBlock
    {
        private readonly GameObject _meshObject;
        public Bounds Bounds;
        public bool IsVisible => _meshObject.activeSelf;

        private MeshFilter _meshFilter;
        
        public TerrainBlock(Vector2 coord, int size, Transform parent, GameObject prefab)
        {
            var position = coord * size;
            Bounds = new Bounds(position, Vector2.one * size);

            _meshObject = Object.Instantiate(prefab, parent);
            _meshObject.transform.position = new Vector3(position.x, 0, position.y);
            Debug.Log(coord + "" + _meshObject.transform.position);
            //_meshObject.transform.localScale = Vector3.one * size;
            _meshObject.name = "Terrain Block";

            _meshFilter = _meshObject.GetComponent<MeshFilter>();
            
            SetVisible(false);
            
            TerrainChannel.RequestMapData(OnMapDataReceived);
        }

        public void SetVisible(bool visible)
        {
            _meshObject.SetActive(visible);
        }

        void OnMapDataReceived(MapData mapData)
        {
            TerrainChannel.RequestMeshData(mapData, OnMeshDataReceived);
        }

        void OnMeshDataReceived(MeshData meshData)
        {
            _meshFilter.mesh = meshData.CreateMesh();
        }
    }
}