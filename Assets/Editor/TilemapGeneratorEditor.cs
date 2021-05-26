using UnityEditor;
using UnityEngine;

namespace DeliveryRush
{
    [CustomEditor(typeof(TilemapGenerator))]
    public class TilemapGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var manager = (TilemapGenerator) target;

            if (GUILayout.Button("Build"))
            {
                manager.Generate(15);
            }
            
            if (GUILayout.Button("Clear"))
            {
                manager.Clear();
            }
        }
    }
}