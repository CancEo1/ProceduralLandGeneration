using UnityEngine;
using System.Collections;
using UnityEditor;

// Custom editor for the MapGenerator script to add a "Generate Map" button in the inspector.
[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGen = (MapGenerator)target;

        if (DrawDefaultInspector())
        {
            // Regenerate the map if any inspector value changes and autoUpdate is enabled
            if (mapGen.autoUpdate)
            {
                mapGen.GenerateMap();
            }
        }

        // Create a custome generate map button
        if (GUILayout.Button("Generate Map"))
        {
            mapGen.GenerateMap();
        }
    }
}
