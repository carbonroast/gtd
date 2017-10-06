using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawn : NetworkBehaviour {
	public GameObject enemyPrefab;
	public Transform spawnPoint;
	public float interval;
	public List<Transform> destination = new List<Transform> ();
	private int waveNumber;
	private float temp;
	// Use this for initialization
	public override void OnStartServer(){

	}

	void Awake(){
		Path ();
		temp = interval;
	}
	// Update is called once per frame
	void Update () {
		if (interval <= 0) {
			SpawnNext ();
			interval = temp;
		}

		interval -= Time.deltaTime;
	}


	void SpawnNext(){
		GameObject enemy = (GameObject)Instantiate (enemyPrefab, spawnPoint.position, spawnPoint.rotation);
		enemy.GetComponent<Enemy> ().spawner = this.gameObject;
		NetworkServer.Spawn (enemy);
	}

	void Path(){

		for (int i = 0; i < transform.childCount; i++) {
			destination.Add (transform.GetChild (i));
		}


	}
}
