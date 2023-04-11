using UnityEngine;

namespace _Scripts
{
    public enum DrawMode
    {
        NoiseMap,
        ColorMap
    }

    public class MapDisplay : MonoBehaviour
    {
        public Renderer textureRenderer;

        public void DrawTexture(float[,] noiseMap)
        {
            var width = noiseMap.GetLength(0);
            var height = noiseMap.GetLength(1);
            
            var texture = TextureGenerator.TextureFromHeightMap(noiseMap, width, height);

            SetTextureOnRenderer(texture, width, height);
        }
        
        public void DrawTexture(Color[] colorMap, int width, int height)
        {
            var texture = TextureGenerator.TextureFromColorMap(colorMap, width, height);

            SetTextureOnRenderer(texture, width, height);
        }

        private void SetTextureOnRenderer(Texture2D texture, int width, int height)
        {
            textureRenderer.sharedMaterial.mainTexture = texture;
            textureRenderer.transform.localScale = new Vector3(width, 1, height);
        }
    }
}