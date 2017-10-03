using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPlaceManager : MonoBehaviour {
	private static Dictionary <string, GameObject> buildPlace = new Dictionary<string, GameObject>();

	public static void RegisterBuildPlace (string _BuildPlaceLocation, GameObject _BuildPlaceLocationGO){
		buildPlace.Add (_BuildPlaceLocation, _BuildPlaceLocationGO);
	}

	public static GameObject GetBuildPlace(string _BuildPlaceLocation){
		return buildPlace [_BuildPlaceLocation];
	}
}
