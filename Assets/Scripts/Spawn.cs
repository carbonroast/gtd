using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {
	public GameObject enemyPrefab;
	public Transform spawnPoint;
	public float interval = 3;

	private int waveNumber;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("SpawnNext",interval,interval);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnNext(){
		Instantiate (enemyPrefab, spawnPoint.position, spawnPoint.rotation);
	}
}
