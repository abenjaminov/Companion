using System;
using UnityEngine.Serialization;

namespace _Scripts.Types
{
    [Serializable]
    public class LevelOfDetailInfo
    {
        public int levelOfDetail;
        public int visibleDistanceThreshold;
    }
}