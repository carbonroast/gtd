using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour {

	private static Dictionary <string, GameObject> projectiles = new Dictionary<string, GameObject>();


	public static void RegisterProjectile (string _netID, GameObject projectileGO){
		string projectileID = projectileGO.transform.name + _netID;
		projectiles.Add (projectileID, projectileGO);
		projectileGO.transform.name = projectileID;
	}


	public static GameObject GetProjectile(string projectileID){
		return projectiles [projectileID];
	}

	public static void DisableProjectile(string projectileID){
		//projectiles.Remove (enemyID);
	}

}
