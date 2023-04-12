using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts
{
    public class TerrainGenerator : MonoBehaviour
    {
        private const float MaxViewDistance = 450;
        public Transform player;

        private static Vector2 _playerPosition;
        private static MapGenerator _mapGenerator;
        private int _terrainBlockSize;
        private int _terrainBlocksVisibleInViewDistance;

        public GameObject terrainBlockPrefab;
        
        private readonly Dictionary<Vector2, TerrainBlock> _terrainBlockDict = new();
        private readonly List<TerrainBlock> _terrainBlocksVisibleLastUpdate = new();
        private void Start()
        {
            _mapGenerator = FindObjectOfType<MapGenerator>();
            _terrainBlockSize = MapGenerator.TerrainBlockSize - 1;
            _terrainBlocksVisibleInViewDistance = Mathf.RoundToInt(MaxViewDistance / _terrainBlockSize);
        }

        void UpdateVisibleTerrainBlocks()
        {
            _terrainBlocksVisibleLastUpdate.ForEach(block => block.SetVisible(false));
            _terrainBlocksVisibleLastUpdate.Clear();
            
            var currentTerrainBlockCoordX = Mathf.RoundToInt(_playerPosition.x / _terrainBlockSize);
            var currentTerrainBlockCoordY = Mathf.RoundToInt(_playerPosition.y / _terrainBlockSize);

            for (var yOffset = -_terrainBlocksVisibleInViewDistance; yOffset <= _terrainBlocksVisibleInViewDistance; yOffset++)
            {
                for (var xOffset = -_terrainBlocksVisibleInViewDistance; xOffset <= _terrainBlocksVisibleInViewDistance; xOffset++)
                {
                    var viewedBlockCoord = new Vector2(currentTerrainBlockCoordX + xOffset, currentTerrainBlockCoordY + yOffset);

                    if (_terrainBlockDict.TryGetValue(viewedBlockCoord, out var value))
                    {
                        UpdateBlockVisibility(value);
                        
                        if (value.IsVisible)
                        {
                            _terrainBlocksVisibleLastUpdate.Add(value);
                        }
                        
                        continue;
                    }

                    var terrainBlock = new TerrainBlock(viewedBlockCoord, _terrainBlockSize, this.transform, terrainBlockPrefab);
                    _terrainBlockDict.Add(viewedBlockCoord, terrainBlock);
                }   
            }
        }
        
        private void UpdateBlockVisibility(TerrainBlock block)
        {
            var playerDistanceFromNearestEdge = Mathf.Sqrt(block.Bounds.SqrDistance(_playerPosition));
            var isBlockVisible = playerDistanceFromNearestEdge <= MaxViewDistance;
            block.SetVisible(isBlockVisible);
        }

        private void Update()
        {
            var playerPositionTemp = player.position;
            _playerPosition = new Vector2(playerPositionTemp.x, playerPositionTemp.z);
            
            UpdateVisibleTerrainBlocks();
        }
    }
}