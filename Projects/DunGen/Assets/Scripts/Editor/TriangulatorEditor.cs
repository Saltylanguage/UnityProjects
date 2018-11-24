using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Triangulator))]
public class TriangulatorEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Triangulator triangulator = (Triangulator)target;

        if(GUILayout.Button("Triangulate"))
        {
            triangulator.TriangulateRooms();
        }
    }

}
