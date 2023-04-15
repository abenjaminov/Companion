using System.Collections.Generic;
using System.Linq;
using _Scripts.TerrainGeneration.Types;
using UnityEngine;

namespace _Scripts.TerrainGeneration
{
    public class TerrainGenerator : MonoBehaviour
    {
        private static float MaxViewDistance;
        public Transform player;
        public float playerMoveThresholdForUpdate = 25;
        public float SquarePlayerMoveThresholdForUpdate => playerMoveThresholdForUpdate * playerMoveThresholdForUpdate;
        
        private static Vector2 _playerPosition = Vector2.zero;
        private static Vector2 _playerPositionOld;
        private bool _isFirstUpdate = true;
        private static MapGenerator _mapGenerator;
        private int _terrainBlockSize;
        private int _terrainBlocksVisibleInViewDistance;

        public GameObject terrainBlockPrefab;

        public List<LevelOfDetailInfo> levelOfDetailInfos;
        
        private readonly Dictionary<Vector2, TerrainBlock> _terrainBlockDict = new();
        private readonly List<TerrainBlock> _terrainBlocksVisibleLastUpdate = new();
        
        private void Start()
        {
            MaxViewDistance = levelOfDetailInfos[^1].visibleDistanceThreshold;
            _mapGenerator = FindObjectOfType<MapGenerator>();
            _terrainBlockSize = MapGenerator.TerrainBlockSize - 1;
            _terrainBlocksVisibleInViewDistance = Mathf.RoundToInt(MaxViewDistance / _terrainBlockSize);

            TerrainChannel.OnUpdateTerrainBlock += UpdateBlockVisibility;
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

                        continue;
                    }

                    var terrainBlock = new TerrainBlock(viewedBlockCoord, 
                        _terrainBlockSize, 
                        transform, 
                        terrainBlockPrefab,
                        levelOfDetailInfos);
                    
                    _terrainBlockDict.Add(viewedBlockCoord, terrainBlock);
                }   
            }
        }

        private void UpdateBlockVisibility(TerrainBlock block)
        {
            var playerDistanceFromNearestEdge = Mathf.Sqrt(block.Bounds.SqrDistance(_playerPosition));
            var isBlockVisible = playerDistanceFromNearestEdge <= MaxViewDistance;

            if (isBlockVisible)
            {
                var levelOfDetails =
                    levelOfDetailInfos.FirstOrDefault(x => playerDistanceFromNearestEdge <= x.visibleDistanceThreshold);
                block.SetDetailLevel(levelOfDetails ?? levelOfDetailInfos[^1]);
            }
            
            if (isBlockVisible)
            {
                _terrainBlocksVisibleLastUpdate.Add(block);
            }
            
            block.SetVisible(isBlockVisible);
        }

        private void Update()
        {
            var playerPositionTemp = player.position;
            _playerPosition = new Vector2(playerPositionTemp.x, playerPositionTemp.z);

            if ((_playerPositionOld - _playerPosition).sqrMagnitude >= SquarePlayerMoveThresholdForUpdate)
            {
                _playerPositionOld = _playerPosition;
                UpdateVisibleTerrainBlocks();
                return;
            }
            
            if (_isFirstUpdate)
            {
                _isFirstUpdate = false;
                UpdateVisibleTerrainBlocks();
            }
        }
    }
}