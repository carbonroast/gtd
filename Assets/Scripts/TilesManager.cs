using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour {
	
	//private const string cubeIdPrefix = "Cube ";
	private static Dictionary <string, GameObject> _tileMap = new Dictionary<string, GameObject>();


	public static void RegisterMap (string _netID, GameObject _tilemapGO){
		string _mapTiles = "MapTile " + _netID;
		_tileMap.Add (_mapTiles, _tilemapGO);
		_tilemapGO.transform.name = _mapTiles;
	}


	public static GameObject GetMap(string name){
		return _tileMap [name];
	}

	public static TDTile GetTile(string name, int x, int y){
		return _tileMap [name].GetComponent<TGMap>().map.GetTileAt(x,y);
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
