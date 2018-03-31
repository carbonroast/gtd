using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TGMap))]
public class TileGraphicInspector : Editor {
	public override void OnInspectorGUI(){
		DrawDefaultInspector ();



		if (GUILayout.Button ("Regenerate")) {
			TGMap tileMap = (TGMap)target;
			tileMap.BuildMesh ();
		}
	}
}
