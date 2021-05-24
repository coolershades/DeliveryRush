using UnityEditor;
using UnityEngine;

namespace DeliveryRush
{
    [CustomEditor(typeof(MapBuilder))]
    public class MapBuilderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var manager = (MapBuilder) target;

            if (GUILayout.Button("Build Random Map"))
            {
                manager.Clear();
                var map = MapGenerator.GenerateTypeMap(MapGenerator.GenerateAreaTypeMap());
                manager.BuildMap(map);
            }

            if (GUILayout.Button("Clear"))
            {
                manager.Clear();
            }
        }
    }
}