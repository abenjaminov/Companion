using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts
{
    [RequireComponent(typeof(MapDisplay))]
    public class MapGenerator : MonoBehaviour
    {
        public bool autoUpdate;
        public int mapSize;
        public float noiseScale;
        public MapDisplay mapDisplay;

        public void GenerateMap()
        {
            var noiseMap = Noise.GenerateNoiseMap(new GenerateNoiseMapOptions()
            {
                Scale = this.noiseScale,
                Size = this.mapSize
            });
            
            this.mapDisplay.DrawNoiseMap(noiseMap);
        }
    }
}