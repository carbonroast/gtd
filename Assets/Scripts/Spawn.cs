using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawn : NetworkBehaviour {
	public GameObject enemyPrefab;
	public Transform spawnPoint;
	public float interval;
	private int waveNumber;
	private float temp;
	// Use this for initialization


	void Start(){
		if (!isServer) {
			return;
		}
		temp = interval;
	}
	// Update is called once per frame
	void Update () {
		if (!isServer) {
			return;
		}
		if (interval <= 0) {
			CmdSpawnNext ();
			interval = temp;
		}

		interval -= Time.deltaTime;
	}

	[Command]
	void CmdSpawnNext(){
		GameObject enemy = (GameObject)Instantiate (enemyPrefab);
		enemy.transform.position = WayPointManager.GetWayPoints (0);
		NetworkServer.Spawn (enemy);

	}


}
