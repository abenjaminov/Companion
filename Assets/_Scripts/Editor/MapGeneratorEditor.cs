using UnityEditor;
using UnityEngine;

namespace _Scripts.Editor
{
    [CustomEditor(typeof(MapGenerator))]
    public class MapGeneratorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var mapGenerator = (MapGenerator)target;

            var isUpdatedGUI = DrawDefaultInspector();
            var isGenerateClicked = GUILayout.Button("Generate Map");
            
            if ((isUpdatedGUI && mapGenerator.autoUpdate) || isGenerateClicked)
            {
                mapGenerator.GenerateMap();
            }
        }
    }
}