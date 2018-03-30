using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TGMap))]
public class TileMapInspector : Editor {
	float v = .5f;
	public override void OnInspectorGUI(){
		DrawDefaultInspector ();

		EditorGUILayout.BeginVertical ();
		v = EditorGUILayout.Slider (v, 0, 2.0f);
		EditorGUILayout.EndVertical ();

		if (GUILayout.Button ("Regenerate")) {
			TGMap tileMap = (TGMap)target;
			tileMap.BuildMesh ();
		}
	}
}
