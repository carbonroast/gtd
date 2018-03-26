using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour {

	private static Dictionary<string, GameObject> towers = new Dictionary<string, GameObject> ();

	//private const string towerIdPrefix = "Tower ";


	public static void RegisterTower (string netID, GameObject towerLocationGO){
		string towerID = towerLocationGO.transform.name + netID;
		towers.Add (towerID, towerLocationGO);
		towerLocationGO.transform.name = towerID;
	}


	public static GameObject GetTower(string towerID){
		return towers [towerID];
	}

	/*
	void OnGUI(){
		GUILayout.BeginArea (new Rect (200, 200, 200, 500));

		GUILayout.BeginVertical ();

		foreach (string _cubeNetId in towers.Keys) {
			GUILayout.Label (_cubeNetId + " - " + towers [_cubeNetId].transform.name);
		}

		GUILayout.EndVertical ();
		GUILayout.EndArea();
	}*/

}
