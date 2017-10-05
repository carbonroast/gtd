using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawn : NetworkBehaviour {
	public GameObject enemyPrefab;
	public Transform spawnPoint;
	public float interval = 3;

	private int waveNumber;
	// Use this for initialization
	public override void OnStartServer(){
		for (int i = 0; i < interval; i++) {
			SpawnNext ();
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnNext(){
		GameObject enemy = (GameObject)Instantiate (enemyPrefab, spawnPoint.position, spawnPoint.rotation);
		NetworkServer.Spawn (enemy);
	}
}
