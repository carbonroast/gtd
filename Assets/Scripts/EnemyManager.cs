using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

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
		
}
