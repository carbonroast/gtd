using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour {
	private static Dictionary <string, GameObject> _tiles = new Dictionary<string, GameObject>();

	public static void RegisterTiles (string _BuildPlaceLocation, GameObject _BuildPlaceLocationGO){
		_tiles.Add (_BuildPlaceLocation, _BuildPlaceLocationGO);
	}

	public static GameObject GetTiles(string _BuildPlaceLocation){
		return _tiles [_BuildPlaceLocation];
	}
}
