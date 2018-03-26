using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	//private const string cubeIdPrefix = "Cube ";
	private static Dictionary <string, GameObject> enimies = new Dictionary<string, GameObject>();


	public static void RegisterEnemy (string _netID, GameObject enemyGO){
		string enemyID = enemyGO.transform.name + _netID;
		enimies.Add (enemyID, enemyGO);
		enemyGO.transform.name = enemyID;
	}


	public static GameObject GetEnemy(string enemyID){
		return enimies [enemyID];
	}

	public static void DestroyEnemy(string enemyID){
		enimies.Remove (enemyID);
	}


	void OnGUI(){
		GUILayout.BeginArea (new Rect (200, 200, 200, 500));

		GUILayout.BeginVertical ();

		foreach (string _cubeNetId in enimies.Keys) {
			GUILayout.Label (_cubeNetId + " - " + enimies [_cubeNetId].transform.name);
		}

		GUILayout.EndVertical ();
		GUILayout.EndArea();
	}
}
