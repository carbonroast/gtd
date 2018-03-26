using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour {
	
	//private const string cubeIdPrefix = "Cube ";
	private static Dictionary <string, GameObject> _tiles = new Dictionary<string, GameObject>();


	public static void RegisterTiles (string _netID, GameObject _BuildPlaceLocationGO){
		string _cubeID = _BuildPlaceLocationGO.transform.name + _netID;
		_tiles.Add (_cubeID, _BuildPlaceLocationGO);
		_BuildPlaceLocationGO.transform.name = _cubeID;
	}


	public static GameObject GetTiles(string _cubeID){
		return _tiles [_cubeID];
	}

	/*
	void OnGUI(){
		GUILayout.BeginArea (new Rect (200, 200, 200, 500));

		GUILayout.BeginVertical ();

		foreach (string _cubeNetId in _tiles.Keys) {
			GUILayout.Label (_cubeNetId + " - " + _tiles [_cubeNetId].transform.name);
		}

		GUILayout.EndVertical ();
		GUILayout.EndArea();
	}*/
}
