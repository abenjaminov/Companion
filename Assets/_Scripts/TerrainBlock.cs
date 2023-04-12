using System.Collections.Generic;
using _Scripts.Types;
using UnityEngine;

namespace _Scripts
{
    public class TerrainBlock
    {
        private readonly GameObject _meshObject;
        public Bounds Bounds;
        public bool IsVisible => _meshObject.activeSelf;

        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private Dictionary<int, LevelOfDetailMesh> _levelOfDetailMeshDetails;

        private MapData _mapData;
        private bool _mapDataReceived;

        private LevelOfDetailInfo previousLevelOfDetailInfo;
        
        public TerrainBlock(Vector2 coord, int size, Transform parent, GameObject prefab, List<LevelOfDetailInfo> detailLevels)
        {
            var position = coord * size;
            Bounds = new Bounds(position, Vector2.one * size);

            _meshObject = Object.Instantiate(prefab, parent);
            _meshObject.transform.position = new Vector3(position.x, 0, position.y);
            //_meshObject.transform.localScale = Vector3.one * size;
            _meshObject.name = "Terrain Block";

            _meshFilter = _meshObject.GetComponent<MeshFilter>();
            _meshRenderer = _meshObject.GetComponent<MeshRenderer>();
            
            SetVisible(false);

            _levelOfDetailMeshDetails = new Dictionary<int, LevelOfDetailMesh>();
            foreach (var detailLevelInfo in detailLevels)
            {
                _levelOfDetailMeshDetails.Add(
                    detailLevelInfo.visibleDistanceThreshold, 
                    new LevelOfDetailMesh(detailLevelInfo.levelOfDetail, this));
            }
            
            TerrainChannel.RequestMapData(position, OnMapDataReceived);
        }

        public void SetVisible(bool visible)
        {
            _meshObject.SetActive(visible);
        }

        public void SetDetailLevel(LevelOfDetailInfo info)
        {
            if (!_mapDataReceived) return;
            
            if (previousLevelOfDetailInfo != null && previousLevelOfDetailInfo.visibleDistanceThreshold == info.visibleDistanceThreshold) return;
            
            var levelOfDetailMesh = _levelOfDetailMeshDetails[info.visibleDistanceThreshold];

            if (levelOfDetailMesh.HasMesh)
            {
                previousLevelOfDetailInfo = info;
                _meshFilter.mesh = levelOfDetailMesh.Mesh;
            }
            else if(!levelOfDetailMesh.HasRequestedMesh)
            {
                levelOfDetailMesh.RequestMesh(_mapData);
            }
        }

        void OnMapDataReceived(MapData mapData)
        {
            _mapDataReceived = true;
            _mapData = mapData;

            var texture = TextureGenerator.TextureFromColorMap(mapData.colorMap, MapGenerator.TerrainBlockSize, MapGenerator.TerrainBlockSize);
            _meshRenderer.material.mainTexture = texture;
            
            TerrainChannel.UpdateTerrainBlock(this);
        }
    }
}