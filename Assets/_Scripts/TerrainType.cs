using System;
using UnityEngine;

namespace _Scripts
{
    [Serializable]
    public struct TerrainType : IComparable<TerrainType>
    {
        public string name;
        public float maxHeight;
        public Color color;
        
        public int CompareTo(TerrainType other)
        {
            if (other.maxHeight > this.maxHeight)
            {
                return -1;
            }
            else if (other.maxHeight < this.maxHeight)
            {
                return 1;
            }

            return 0;
        }
    }
}