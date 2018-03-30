using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour {

	private static Dictionary<string, GameObject> towers = new Dictionary<string, GameObject> ();

	public static void RegisterTower (string netID, GameObject towerLocationGO){
		string towerID = towerLocationGO.transform.name + netID;
		towers.Add (towerID, towerLocationGO);
		towerLocationGO.transform.name = towerID;
	}
		
	public static GameObject GetTower(string towerID){
		return towers [towerID];
	}
		
}
