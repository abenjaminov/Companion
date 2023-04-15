using System;

namespace _Scripts.TerrainGeneration.Types
{
    [Serializable]
    public class LevelOfDetailInfo
    {
        public int levelOfDetail;
        public int visibleDistanceThreshold;
    }
}