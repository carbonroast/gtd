using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class Tile : NetworkBehaviour {
	[SyncVar] public bool canBuild;

	// Use this for initialization
	void Start () {
		canBuild = true;
	}

	public void CanBuild(bool _canBuild){
		
		canBuild = _canBuild;
		Debug.LogError ("canbuild= " + canBuild);
	}
}
